using System;
using System.Diagnostics;
using System.Linq;

namespace Ch04.Examples
{
    public class _HandlingException : IBaseExecutor
    {
        private readonly ParallelQuery<int> ParallelQueryRange = ParallelEnumerable.Range(1, 500);

        public void Run()
        {
            //PropagateBackToTheCaller();
            DelegateHandle();
        }

        private void DelegateHandle()
        {
            Func<int, int> SelectDivision = (i) =>
            {
                try
                {
                    return i / (i - 10);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@$"{Environment.NewLine}Type Exception: {ex.GetType().Name} | Message: {ex.Message} | StackTrace: {ex.StackTrace} |");
                    return -1;
                }
            };

            Stopwatch watch = null;

            ParallelQuery<int> query = ParallelQueryRange.Select(i => SelectDivision(i)).WithDegreeOfParallelism(2);

            watch = Stopwatch.StartNew();

            try
            {
                query.ForAll(item => Console.Write($"{item}:{watch.ElapsedMilliseconds} |"));
            }
            catch (AggregateException aggregateException)
            {
                foreach (var ex in aggregateException.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                    if (ex is DivideByZeroException)
                        Console.WriteLine("Attempt to divide by zero. Query stopped.");
                }
            }

            Console.WriteLine($"{Environment.NewLine}Full Result returned in { watch.ElapsedMilliseconds} ms{Environment.NewLine}");
        }

        private void PropagateBackToTheCaller()
        {
            ParallelQuery<int> query = ParallelQueryRange.Select(i => i / (i - 10)).WithDegreeOfParallelism(2);

            try
            {
                query.ForAll(i => Console.WriteLine(i));
            }
            catch (AggregateException aggregateException)
            {
                foreach (var ex in aggregateException.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                    if (ex is DivideByZeroException)
                        Console.WriteLine("Attempt to divide by zero. Query stopped.");
                }
            }
        }
    }
}
