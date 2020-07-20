using System;
using System.Threading.Tasks;

namespace Ch02.Examples
{
    public class _TaskWhenAny : IBaseExecutor
    {
        public void Run()
        {
            Task taskA = Task.Factory.StartNew(() =>
            {
                Task.Delay(2000).Wait();
                Console.WriteLine("WhenAny | TaskA finished");
            });

            Task taskB = Task.Factory.StartNew(() => Console.WriteLine("WhenAny | TaskB finished"));

            Task taskC = Task.Factory.StartNew(() =>
            {
                Task.Delay(1000).Wait();
                Console.WriteLine("WhenAny | TaskC finished");
            });

            Task.WhenAny(taskA, taskB, taskC);
            Console.WriteLine("WhenAny | Calling method finishes");
        }
    }
}
