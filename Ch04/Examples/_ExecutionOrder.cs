using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ch04.Examples
{
    public class _ExecutionOrder : IBaseExecutor
    {
        private static readonly IEnumerable<int> range = Enumerable.Range(1, 10);

        public void Run()
        {
            //RunSequentialOrdered();
            //RunParallelUnordered();
            //RunParallelOrdered();
            RunParallelOrderedUnordered();
        }

        private void RunParallelOrderedUnordered()
        {
            var range = Enumerable.Range(100, 10000);

            var squares = range.AsParallel() // Define Parallelism to Run
                            .AsOrdered().Take(100) // Get the first 100 numbers by order
                            .AsUnordered().Select(i => i * i).ToList(); // Execute Unordered to improve performance

            squares.ForEach(i => Console.Write($"{i}-"));
        }

        private void RunParallelOrdered()
        {
            Console.WriteLine($"{Environment.NewLine}Parallel Ordered");
            var ordered = range.AsParallel().AsOrdered().Select(i => i).ToList();
            ordered.ForEach(i => Console.Write($"{i}-"));
        }

        private void RunParallelUnordered()
        {
            Console.WriteLine($"{Environment.NewLine}Parallel Unordered");
            var unordered = range.AsParallel().Select(i => i).ToList();
            unordered.ForEach(i => Console.Write($"{i}-"));
        }

        private void RunSequentialOrdered()
        {
            Console.WriteLine($"{Environment.NewLine}Sequential Ordered");
            range.ToList().ForEach(i => Console.Write($"{i}-"));
        }
    }
}
