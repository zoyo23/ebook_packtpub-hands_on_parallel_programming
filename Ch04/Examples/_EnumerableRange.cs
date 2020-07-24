using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Ch04.Examples
{
    public class _EnumerableRange : IBaseExecutor
    {
        public void Run()
        {
            ParallelEnumerableVsEnumerable();
        }

        private void ParallelEnumerableVsEnumerable()
        {
            var endRange = int.MaxValue;
            Stopwatch watch = Stopwatch.StartNew();
            IEnumerable<int> parallelRange = ParallelEnumerable.Range(0, endRange).Select(i => i);
            watch.Stop();
            Console.WriteLine($"ParallelEnumerable.Range | Range: {endRange} | Time elapsed {watch.ElapsedMilliseconds} ms");

            Stopwatch watch2 = Stopwatch.StartNew();
            IEnumerable<int> range = Enumerable.Range(0, endRange);
            watch2.Stop();
            Console.WriteLine($"Enumerable.Range | Range: {endRange} | Time elapsed {watch.ElapsedMilliseconds} ms");
        }
    }
}
