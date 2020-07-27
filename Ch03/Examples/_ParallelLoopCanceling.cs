using Ch03;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ch03.Examples
{
    public class _ParallelLoopCanceling : IBaseExecutor
    {
        public void Run()
        {
            //parallelLoopStateBreak();
            //parallelLoopStateBreak2();
            //ParallelLoopStateStop();
            ParallelLoopCancellationToken();
        }

        private void ParallelLoopCancellationToken()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
                cancellationTokenSource.Cancel();
                Console.WriteLine("Token has been cancelled");
            });

            ParallelOptions loopOptions = new ParallelOptions()
            {
                CancellationToken = cancellationTokenSource.Token
            };

            try
            {
                Parallel.For(0, Int64.MaxValue, loopOptions, index =>
                {
                    Thread.Sleep(3000);
                    double result = Math.Sqrt(index);
                    Console.WriteLine($"Index {index}, result {result}");
                });
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Cancellation exception caught!");
            }


        }

        private void ParallelLoopStateStop()
        {
            var numbers = Enumerable.Range(1, 1000);
            Parallel.ForEach(numbers, (i, parallelLoopState) =>
            {
                Console.Write(i + " ");
                if (i % 4 == 0)
                {
                    Console.WriteLine($"Loop Stopped on {i}");
                    parallelLoopState.Stop();
                }
            });
        }

        private void parallelLoopStateBreak2()
        {
            var numbers = Enumerable.Range(1, 1000);
            Parallel.ForEach(numbers, (i, parallelLoopState) =>
            {
                Console.WriteLine($"For i={i} LowestBreakIteration = {parallelLoopState.LowestBreakIteration} and Task id ={Task.CurrentId}");
                if (i >= 10)
                {
                    parallelLoopState.Break();
                }
            });
        }

        private void parallelLoopStateBreak()
        {
            var numbers = Enumerable.Range(1, 1000);
            int numToFind = 2;

            Parallel.ForEach(numbers, (number, parallelLoopState) =>
            {
                Console.Write(number + "-");
                if (number == numToFind)
                {
                    Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}########## Calling Break at {number} ##########{Environment.NewLine}");
                    parallelLoopState.Break();
                }
            });
        }
    }
}
