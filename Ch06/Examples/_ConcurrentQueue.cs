using Common.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ch06.Examples
{
    public class _ConcurrentQueue : IBaseExecutor
    {
        private const int StartRange = 0;
        private const int FinalRange = 500;

        public void Run()
        {
            //ProducerConsumerProblem();
            ProducerConsumerProblemSolvedUsingLockerMonitor();
            //ProducerConsumerProblemSolvedUsingConcurrentQueues();
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
    }
}
