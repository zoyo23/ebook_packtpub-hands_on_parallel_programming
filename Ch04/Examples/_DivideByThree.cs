using System;
using System.Linq;

namespace Ch04
{
    public class _DivideByThree : IBaseExecutor
    {

        public void Run()
        {
            DivideByThree();
        }
        private static void DivideByThree()
        {
            var range = Enumerable.Range(1, 100000);

            //Here is sequential version
            var resultList = range.Where(i => i % 3 == 0).ToList();
            Console.WriteLine($"Sequential: Total items are {resultList.Count}");

            //Here is Parallel Version using .AsParallel method
            resultList = range.AsParallel().Where(i => i % 3 == 0).ToList();
            Console.WriteLine($"Parallel 1: Total items are {resultList.Count}");

            //Here is another Parallel Version using .AsParallel method
            resultList = (from i in range.AsParallel()
                          where i % 3 == 0
                          select i).ToList();
            Console.WriteLine($"Parallel 2: Total items are {resultList.Count}");
        }

    }
}
