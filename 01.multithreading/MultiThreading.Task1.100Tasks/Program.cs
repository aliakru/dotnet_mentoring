/*
 * 1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.
 * Each Task should iterate from 1 to 1000 and print into the console the following string:
 * “Task #0 – {iteration number}”.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task1._100Tasks
{
    class Program
    {
        const int TaskAmount = 100;
        const int MaxIterationsCount = 1000;

        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. Multi threading V1.");
            Console.WriteLine("1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.");
            Console.WriteLine("Each Task should iterate from 1 to 1000 and print into the console the following string:");
            Console.WriteLine("“Task #0 – {iteration number}”.");
            Console.WriteLine();
            
            HundredTasks();

            Console.ReadLine();
        }

        static void HundredTasks()
        {
            var tasks = new List<Task>(10);

            for (int i = 0; i < 10; i++)
            {
                var taskNumber = i;
                tasks.Add(
                    Task.Run(
                    () =>
                    {
                        for (int k = 1; k < 6; k++)
                        {
                            Output(taskNumber, k);
                        }
                    }));
            }

            Task.WhenAll(tasks);
            Console.ReadLine();
        }

        static void HundredTasksSync()
        {
            for (int i = 0; i < 11; i++)
            {
                for (int k = 1; k < 6; k++)
                {
                    Output(i, k);
                }
            }
        }

        static void Output(int taskNumber, int iterationNumber)
        {
            Console.WriteLine($"Task #{taskNumber} – {iterationNumber}     ");
        }

        //static async Task HundredTasks()
        //{
        //    var tasks = new List<Task>();
        //    for (int i = 0; i < 10; i++)
        //    {
        //        tasks.Add(Task.Run(() => Logic(i)));
        //    }

        //    await Task.WhenAll(tasks);
        //}

        //static void Logic(int taskNumber) 
        //{
        //    for (int i = 1; i < 11; i++)
        //    {
        //        Output(taskNumber, i);
        //    }
        //}

        //static void HundredTasks()
        //{
        //    // feel free to add your code here
        //    var tasks = new List<Task>(100);

        //    for (int i = 0;i < 10; i++)
        //    {
        //        tasks.Add(Task.Run(
        //            () =>
        //            {
        //                for (int k = 1; k < 10; k++)
        //                {
        //                    Output(i, k);
        //                }
        //            }));
        //    }

        //    Task.WhenAll(tasks);
        //    Console.ReadLine();
        //}
    }
}
