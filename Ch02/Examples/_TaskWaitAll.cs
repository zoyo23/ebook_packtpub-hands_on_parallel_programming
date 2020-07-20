using System;
using System.Threading.Tasks;

namespace Ch02.Examples
{
    public class _TaskWaitAll : IBaseExecutor
    {
        public void Run()
        {
            Task taskA = Task.Factory.StartNew(() =>
            {
                Task.Delay(2000).Wait();
                Console.WriteLine("WaitAll | TaskA finished");
            });

            Task taskB = Task.Factory.StartNew(() => Console.WriteLine("WaitAll | TaskB finished"));

            Task taskC = Task.Factory.StartNew(() =>
            {
                Task.Delay(1000).Wait();
                Console.WriteLine("WaitAll | TaskC finished");
            });

            Task.WaitAll(taskA, taskB, taskC);
            Console.WriteLine("WaitAll | Calling method finishes");
        }
    }
}
