using Ch02;
using System;
using System.Threading.Tasks;

namespace Ch02.Examples
{
    public class _2GettingResultFromTasks : IBaseExecutor
    {
        public void Run()
        {
            GetResultsFromTasks();
            Console.ReadLine();
        }

        private static void GetResultsFromTasks()
        {
            var sumTaskViaTaskOfInt = new Task<int>(() => Sum(5));
            sumTaskViaTaskOfInt.Start();
            Console.WriteLine($"Result from sumTask is { sumTaskViaTaskOfInt.Result} ");

            var sumTaskViaFactory = Task.Factory.StartNew<int>(() => Sum(5));
            Console.WriteLine($"Result from sumTask is { sumTaskViaFactory.Result} ");

            var sumTaskViaTaskRun = Task.Run<int>(() => Sum(5));
            Console.WriteLine($"Result from sumTask is { sumTaskViaTaskRun.Result} ");

            var sumTaskViaTaskResult = Task.FromResult<int>(Sum(5));
            Console.WriteLine($"Result from sumTask is { sumTaskViaTaskResult.Result} ");
        }

        private static int Sum(int n)
        {
            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += i;
            }
            return sum;
        }
    }
}
