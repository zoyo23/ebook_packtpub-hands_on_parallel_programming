using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ch04.Examples
{
    public class _CancelingPLINQQueries : IBaseExecutor
    {
        private readonly ParallelQuery<int> ParallelQueryRange = ParallelEnumerable.Range(1, 1000000);

        public void Run()
        {
            CancellationTokenCancelQuerie();
        }

        private void CancellationTokenCancelQuerie()
        {
            CancellationTokenSource cs = new CancellationTokenSource();

            // Create a task that starts immediately and cancel the token after 4 seconds
            Task cancellationTask = Task.Factory.StartNew(() =>
            {
                var sleepTime = TimeSpan.FromSeconds(5);
                Console.WriteLine($"Waiting For: {sleepTime} ms");
                Thread.Sleep(sleepTime);
                cs.Cancel();
            });

            try
            {
                var result = ParallelQueryRange.AsParallel()
                                .WithCancellation(cs.Token)
                                .Select(number =>
                                {
                                    Task.Delay(1000).Wait();
                                    Console.WriteLine($"Number: {number} | Task: {Task.CurrentId}");
                                    return number;
                                }).ToList();
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (AggregateException ex)
            {
                foreach (var inner in ex.InnerExceptions)
                {
                    Console.WriteLine(inner.Message);
                }
            }

        }
    }
}
