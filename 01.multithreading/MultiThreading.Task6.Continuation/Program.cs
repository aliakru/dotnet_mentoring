/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            // feel free to add your code

            // A.
            var mainTask = Task.Run(() => Task.Delay(500));

            var task1 = mainTask.ContinueWith(t => Console.WriteLine("Task a. is running."));

            var failingTask = Task.Run(() => throw new Exception())
                .ContinueWith(t => Console.WriteLine("Task a. is running after failing main task."));


            // B.
            
            var parentSuccess = Task.Run(() => Task.Delay(500))
                .ContinueWith(
                    t => Console.WriteLine($"Task B. is running after main task. IsFaulted parent: {t.IsFaulted}"),
                TaskContinuationOptions.OnlyOnFaulted);

            var parentFailing = Task.Run(() => throw new Exception($"Failed expectedly ^_^ (Thread# {Thread.CurrentThread.ManagedThreadId})"))
                .ContinueWith(
                    t => Console.WriteLine($"Task B. is running after failing main task. Error from parent: {t.Exception.Message}.Thread: {Thread.CurrentThread.ManagedThreadId}."),
                TaskContinuationOptions.OnlyOnFaulted);

            // C.

            var parentSuccessC = Task.Run(() => Task.Delay(500))
                .ContinueWith(
                t => Console.WriteLine("Task C. is running after main task."),
                TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);

            var parentFailingC = Task.Run(() => throw new Exception($"Failed expectedly C Task. Thread# {Thread.CurrentThread.ManagedThreadId}."))
                .ContinueWith(
                t => Console.WriteLine($"Task C thread# {Thread.CurrentThread.ManagedThreadId}. Error from parent: {t.Exception.Message}."),
                TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);

            // D.
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            
            tokenSource.Cancel();
            var parentFailingD = Task.Run(
                () => throw new Exception($"Failed expectedly D Task. Is ThreadPool# {Thread.CurrentThread.IsThreadPoolThread}."),
                token)
                .ContinueWith(t =>
                    Console.WriteLine($"Task D is tp# {Thread.CurrentThread.IsThreadPoolThread}. Error from parent: {t.Exception?.Message}."),
                TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.LongRunning);

            //tokenSource.Cancel();
            Console.ReadLine();
        }
    }
}
