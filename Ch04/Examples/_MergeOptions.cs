using Common.Interfaces;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Ch04.Examples
{
    public class _MergeOptions : IBaseExecutor
    {
        private readonly ParallelQuery<int> ParallelQueryRange = ParallelEnumerable.Range(1, 500);

        public void Run()
        {
            NotBufferedMerge();
            AutoBufferedMerge();
            FullyBufferedMerge();
        }

        private void FullyBufferedMerge()
        {
            Stopwatch watch = null;

            ParallelQuery<int> fullyBufferedQuery = ParallelQueryRange.WithMergeOptions(ParallelMergeOptions.FullyBuffered)
                                        .Where(i => i % 10 == 0)
                                        .Select(x =>
                                        {
                                            Thread.SpinWait(1000);
                                            return x;
                                        });

            watch = Stopwatch.StartNew();

            foreach (var item in fullyBufferedQuery)
            {
                Console.Write($"{item}:{watch.ElapsedMilliseconds} |");
            }

            Console.WriteLine($"{Environment.NewLine}FullyBuffered Full Result returned in { watch.ElapsedMilliseconds} ms{Environment.NewLine}");
            watch.Stop();
        }

        private void AutoBufferedMerge()
        {
            Stopwatch watch = null;

            ParallelQuery<int> query = ParallelQueryRange.WithMergeOptions(ParallelMergeOptions.AutoBuffered)
                                        .Where(i => i % 10 == 0)
                                        .Select(x =>
                                        {
                                            Thread.SpinWait(1000);
                                            return x;
                                        });

            watch = Stopwatch.StartNew();

            foreach (var item in query)
            {
                Console.Write($"{item}:{watch.ElapsedMilliseconds} |");
            }

            Console.WriteLine($"{Environment.NewLine}AutoBuffered Full Result returned in { watch.ElapsedMilliseconds} ms{Environment.NewLine}");
            watch.Stop();
        }

        private void NotBufferedMerge()
        {
            Stopwatch watch = null;

            ParallelQuery<int> notBufferedQuery = ParallelQueryRange
                                                    .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                                                    .Where(i => i % 10 == 0)
                                                    .Select(x =>
                                                    {
                                                        Thread.SpinWait(1000);
                                                        return x;
                                                    });

            watch = Stopwatch.StartNew();

            foreach (var item in notBufferedQuery)
            {
                Console.Write($"{item}:{watch.ElapsedMilliseconds} |");
            }

            Console.WriteLine($"{Environment.NewLine}NotBuffered Full Result returned in {watch.ElapsedMilliseconds} ms{Environment.NewLine}");
            watch.Stop();
        }
    }
}
