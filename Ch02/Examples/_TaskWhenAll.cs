using System;
using System.Threading.Tasks;

namespace Ch02.Examples
{
    public class _TaskWhenAll : IBaseExecutor
    {
        public void Run()
        {
            Task taskA = Task.Factory.StartNew(() =>
            {
                Task.Delay(2000).Wait();
                Console.WriteLine("WhenAll | TaskA finished");
            });

            Task taskB = Task.Factory.StartNew(() => Console.WriteLine("WhenAll | TaskB finished"));

            Task taskC = Task.Factory.StartNew(() =>
            {
                Task.Delay(1000).Wait();
                Console.WriteLine("WhenAll | TaskC finished");
            });

            Task.WhenAll(taskA, taskB, taskC);
            Console.WriteLine("WhenAll | Calling method finishes");
        }
    }
}
