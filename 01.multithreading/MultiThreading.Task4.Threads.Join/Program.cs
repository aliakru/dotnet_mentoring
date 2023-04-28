/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            // feel free to add your code

            //ThreadRecursiveAction(10);
            ThreadPoolRecursiveAction(10);
            Console.ReadLine();
        }

        static void ThreadRecursiveAction(object number)
        {
            var id = (int) number;
            if (id >= 0) 
            {
                id--;
                var t = new Thread(ThreadRecursiveAction);
                t.Name = $"Thread #{id}";

                t.Start(id);
                Console.WriteLine(Thread.CurrentThread.Name);

                t.Join();
            }
        }

        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(3);

        static void ThreadPoolRecursiveAction(object number)
        {
            var id = (int)number;

            if (id > 0)
            {
                id--;

                ThreadPool.QueueUserWorkItem(ThreadPoolRecursiveAction, id);
            }

            Console.WriteLine($"Thread #{id} is waiting.");
            semaphoreSlim.Wait();
            Thread.Sleep(1000/id);
            Console.WriteLine($"Thread #{id} is started execution.");
            Console.WriteLine($"Thread #{id} has left.");
        }
    }
}
