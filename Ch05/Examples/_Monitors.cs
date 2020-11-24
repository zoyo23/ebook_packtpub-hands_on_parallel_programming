using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ch05.Examples
{
    public class _Monitors : IBaseExecutor
    {
        private IEnumerable<int> Range = Enumerable.Range(1, 100);

        public void Run()
        {
            //LockExample();
            //MutexExample();
            SemaphoreExample();
        }

        #region Semaphore
        Semaphore Semaphore = new Semaphore(10, 10); //Local Semaphore

        // Any semaphore that is created with a name will be created as a global semaphore
        //Semaphore Semaphore = new Semaphore(5, 5, "Globalsemaphore"); //Global Semaphore

        private void SemaphoreExample()
        {
            Range.AsParallel().ForAll(i =>
            {
                Semaphore.WaitOne();
                Console.WriteLine($"{Environment.NewLine}Index {i} making service call using Task {Task.CurrentId}");

                //Simulate Http call
                try
                {
                    DummyService(i);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{Environment.NewLine}-------------------- Exception Handled on {Task.CurrentId} | {e.Message} --------------------");
                }

                Console.WriteLine($"Index {i} releasing semaphore using Task {Task.CurrentId}");
                Semaphore.Release();
            });
        }

        private static void DummyService(int i)
        {
            var rand = new Random().Next(1000, 1010);
            Console.WriteLine($"Index {i} waiting for: {rand} ms, using Task {Task.CurrentId}");
            if (rand.Equals(1005))
            {
                Console.WriteLine($"######## Index {i} throw an Exception, using Task {Task.CurrentId} ########");
                throw new Exception("1005 Exception");
            }
            Thread.Sleep(rand);
        }
        #endregion

        #region Mutex
        //if we try to run it on multiple instances with unnamedMutex, it will throw an IOException
        //private static Mutex mutex = new Mutex();

        private static Mutex mutex = new Mutex(false, "ShaktiSinghTanwar");
        private void MutexExample()
        {
            Stopwatch watch = Stopwatch.StartNew();

            Range.AsParallel().AsOrdered().ForAll(i =>
            {
                Thread.Sleep(10);
                mutex.WaitOne();
                File.AppendAllText("MutexExample.txt", $"{i} | ");
                mutex.ReleaseMutex();
            });

            watch.Stop();
            Console.WriteLine($"Total time to write file is { watch.ElapsedMilliseconds} ms");
        }
        #endregion

        #region Lock
        static object _locker = new object();

        private void LockExample()
        {
            Stopwatch watch = Stopwatch.StartNew();

            //NonparallelAppendText(range);

            LockParallelAppendText(Range);

            watch.Stop();
            Console.WriteLine($"Total time to write file is { watch.ElapsedMilliseconds} ms");
        }

        private static void NonparallelAppendText(IEnumerable<int> range)
        {
            for (int i = 0; i < range.Count(); i++)
            {
                Thread.Sleep(10);
                File.AppendAllText("LockExample.txt", $"{i} | ");
            }
        }

        private static void LockParallelAppendText(IEnumerable<int> range)
        {
            range.AsParallel().AsOrdered().ForAll(i =>
            {
                Thread.Sleep(10);
                lock (_locker)
                {
                    Thread.Sleep(10);
                    File.AppendAllText("LockExample.txt", $"{i} | ");
                }
            });
        }
        #endregion
    }
}
