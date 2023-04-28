/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        private static List<int> _collection = new List<int>();
        private static ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();

        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            // feel free to add your code
            var rnd = new Random();
            
            var readThread = new Thread(() =>
            {
                
            });
            readThread.IsBackground= true;

            var write = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    _rwLock.EnterWriteLock();
                    var newElement = rnd.Next(1, 50);

                    _collection.Add(newElement);
                    Console.WriteLine($"Element - {newElement} -  is added;");
                    _rwLock.ExitWriteLock();

                    var state = _collection.ToArray();
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ReadCollection), state);
                }
            });

            write.Start();
            Console.ReadLine();
        }

        private static void ReadCollection(object collection) 
        {
            var items = (int[])collection;
            _rwLock.EnterReadLock();
            Console.WriteLine(string.Join(" ", items));
            _rwLock.ExitReadLock();
        }
    }
}
