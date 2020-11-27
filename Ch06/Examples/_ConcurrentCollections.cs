using Common.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ch06.Examples
{
    public class _ConcurrentCollections : IBaseExecutor
    {
        private const int StartRange = 0;
        private const int FinalRange = 500;

        public void Run()
        {
            //ProducerConsumerProblem();
            //ProducerConsumerProblemSolvedUsingLockerMonitor();
            //ProducerConsumerProblemSolvedUsingConcurrentQueues();
            //ProducerConsumerProblemSolvedUsingConcurrentStack();
            //ConcurrentBagDemo();
            BlockingCollectionDemo();
        }

        private void ProducerConsumerProblem()
        {
            Queue<int> queue = EnqueByRange(StartRange, FinalRange);

            int sum = 0;

            Parallel.For(StartRange, FinalRange, (i) =>
            {
                int localSum = 0;
                int localValue;
                while (queue.TryDequeue(out localValue))
                {
                    Thread.Sleep(10);
                    localSum += localValue;
                }
                Interlocked.Add(ref sum, localSum);
            });
            Console.WriteLine($"Calculated Sum is {sum} and should be { Enumerable.Range(StartRange, FinalRange).Sum()}");
        }

        #region Producer Consumer Problem Soluctions
        private void ProducerConsumerProblemSolvedUsingLockerMonitor()
        {
            Queue<int> queue = EnqueByRange(StartRange, FinalRange);

            int sum = 0;
            var _locker = new object();

            Parallel.For(StartRange, FinalRange, (i) =>
            {
                int localSum = 0;
                int localValue;
                Monitor.Enter(_locker);
                while (queue.TryDequeue(out localValue))
                {
                    Thread.Sleep(10);
                    localSum += localValue;
                }
                Monitor.Exit(_locker);
                Interlocked.Add(ref sum, localSum);
            });
            Console.WriteLine($"Calculated Sum is {sum} and should be { Enumerable.Range(StartRange, FinalRange).Sum()}");
        }

        private void ProducerConsumerProblemSolvedUsingConcurrentQueues()
        {
            ConcurrentQueue<int> queue = EnqueConcurrentQueueByRange(StartRange, FinalRange);

            int sum = 0;

            Parallel.For(StartRange, FinalRange, (i) =>
            {
                int localSum = 0;
                int localValue;
                while (queue.TryDequeue(out localValue))
                {
                    Thread.Sleep(10);
                    localSum += localValue;
                }
                Interlocked.Add(ref sum, localSum);
            });
            Console.WriteLine($"Calculated Sum is {sum} and should be { Enumerable.Range(StartRange, FinalRange).Sum()}");
        }

        private void ProducerConsumerProblemSolvedUsingConcurrentStack()
        {
            ConcurrentStack<int> concurrentStack = EnqueConcurrentStackByRange(StartRange, FinalRange);

            int sum = 0;

            Parallel.For(StartRange, FinalRange, (i) =>
            {
                int localSum = 0;
                int localValue;
                while (concurrentStack.TryPop(out localValue))
                {
                    Thread.Sleep(10);
                    localSum += localValue;
                }
                Interlocked.Add(ref sum, localSum);
            });
            Console.WriteLine($"Calculated Sum is {sum} and should be { Enumerable.Range(StartRange, FinalRange).Sum()}");
        }

        private static Queue<int> EnqueByRange(int start, int final)
        {
            Queue<int> queue = new Queue<int>();

            for (int i = start; i < final; i++)
            {
                queue.Enqueue(i);
            }

            return queue;
        }

        private static ConcurrentQueue<int> EnqueConcurrentQueueByRange(int start, int final)
        {
            ConcurrentQueue<int> queue = new ConcurrentQueue<int>();

            for (int i = start; i < final; i++)
            {
                queue.Enqueue(i);
            }

            return queue;
        }

        private static ConcurrentStack<int> EnqueConcurrentStackByRange(int start, int final)
        {
            ConcurrentStack<int> queue = new ConcurrentStack<int>();

            for (int i = start; i < final; i++)
            {
                queue.Push(i);
            }

            return queue;
        }
        #endregion

        #region Demos
        private void ConcurrentBagDemo()
        {
            ConcurrentBag<int> concurrentBag = new ConcurrentBag<int>();

            ManualResetEventSlim manualResetEvent = new ManualResetEventSlim(false);

            Task consumerTask = Task.Factory.StartNew(() =>
            {
                for (int i = 1; i <= 3; ++i)
                {
                    concurrentBag.Add(i);
                }
                //Allow second thread to add items
                manualResetEvent.Wait();
                while (concurrentBag.IsEmpty == false)
                {
                    int item;
                    if (concurrentBag.TryTake(out item))
                    {
                        Console.WriteLine($"Item is {item}");
                    }
                }
            });

            Task producerTask = Task.Factory.StartNew(() =>
            {
                for (int i = 4; i <= 6; ++i)
                {
                    concurrentBag.Add(i);
                }
                manualResetEvent.Set();
            });

            Task.WaitAll(consumerTask, producerTask);
        }

        private void BlockingCollectionDemo()
        {
            BlockingCollection<int> blockingCollection = new BlockingCollection<int>(10);

            Task producerTask = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 5; ++i)
                {
                    blockingCollection.Add(i);
                }
                blockingCollection.CompleteAdding();
            });

            Task consumerTask = Task.Factory.StartNew(() =>
            {
                while (!blockingCollection.IsCompleted)
                {
                    int item = blockingCollection.Take();
                    Console.WriteLine($"Item retrieved is {item}");
                }
            });


            Task.WaitAll(consumerTask, producerTask);
        }
        #endregion
    }
}
