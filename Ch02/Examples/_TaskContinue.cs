using Common.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ch02.Examples
{
    public class _TaskContinue : IBaseExecutor
    {
        public void Run()
        {
            //ContinueWhenAll();

            ContinueWhenAny();
        }

        private async static void ContinueWhenAll()
        {
            int a = 2, b = 3;

            Task<int> taskA = Task.Factory.StartNew<int>(() => a * a);
            Task<int> taskB = Task.Factory.StartNew<int>(() => b * b);
            Task<int> taskC = Task.Factory.StartNew<int>(() => 2 * a * b);

            var sum = await Task.Factory.ContinueWhenAll<int>(new Task[] { taskA, taskB, taskC }, (tasks) => tasks.Sum(t => (t as Task<int>).Result));

            Console.WriteLine(sum);
        }

        private static void ContinueWhenAny()
        {
            int number = 13;
            Task<bool> taskA = Task.Factory.StartNew<bool>(() =>
            number / 2 != 0);
            Task<bool> taskB = Task.Factory.StartNew<bool>(() =>
            (number / 2) * 2 != number);
            Task<bool> taskC = Task.Factory.StartNew<bool>(() =>
            (number & 1) != 0);
            Task.Factory.ContinueWhenAny<bool>(new Task<bool>[] { taskA, taskB, taskC }, (task) =>
            {
                Console.WriteLine((task as Task<bool>).Result);
            });
        }

    }
}
