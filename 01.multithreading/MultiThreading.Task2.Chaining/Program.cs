/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            // feel free to add your code
            var arrayTask = Task.Run(() =>
            {
                var integers = GetArray();
                Print("Initial array:", integers);

                return integers;
            });

            var task2 = arrayTask.ContinueWith(antecedent => 
            {
                int[] integers = antecedent.Result;
                var random = new Random();
                var multiplier = random.Next(1, 25);
                Print("Multiplier:", multiplier);

                var result = integers.Select(item => item * multiplier).ToArray();

                Print("Multiplied array:", result);
                return result;
            });

            var task3 = task2.ContinueWith(antecedent =>
            {
                int[] integers = antecedent.Result;

                Array.Sort(integers);

                Print("Sorted array:", integers);
                return integers;
            });

            var task4 = task3.ContinueWith(antecedent =>
            {
                int[] integers = antecedent.Result;
                var sum = 0;
                foreach (var item in integers) { sum += item; }

                return sum / integers.Length;
            });

            Print("Average:", task4.Result);
            Console.ReadLine();
        }

        static int[] GetArray()
        {
            var random = new Random();
            var arrayOfInt = new int[10];

            for (int i = 0; i < arrayOfInt.Length; i++)
            {
                arrayOfInt[i] = random.Next(0, 150);
            }

            return arrayOfInt;
        }

        static void Print(string msg, params int[] array) 
        {
            Console.WriteLine($"{msg} {String.Join(", ", array)}");
        }
    }
}
