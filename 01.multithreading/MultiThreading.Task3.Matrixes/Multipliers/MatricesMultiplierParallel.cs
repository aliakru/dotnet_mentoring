﻿using MultiThreading.Task3.MatrixMultiplier.Matrices;
using System.Threading.Tasks;

namespace MultiThreading.Task3.MatrixMultiplier.Multipliers
{
    public class MatricesMultiplierParallel : IMatricesMultiplier
    {
        public IMatrix Multiply(IMatrix m1, IMatrix m2)
        {
            // todo: feel free to add your code here
            var lockObj = new object();

            var resultMatrix = new Matrix(m1.RowCount, m2.ColCount);
            Parallel.For(0, m1.RowCount, i =>
            {
                Parallel.For(0, m2.ColCount, j => 
                {
                    long sum = 0;
                    Parallel.For(0, m1.ColCount, k => 
                    {
                        lock (lockObj)
                        {
                            sum += m1.GetElement(i, k) * m2.GetElement(k, j);
                        }
                    });

                    resultMatrix.SetElement(i, j, sum);
                });
            });

            return resultMatrix;
        }
    }
}
