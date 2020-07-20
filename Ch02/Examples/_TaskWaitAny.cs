using System;
using System.Threading.Tasks;

namespace Ch02.Examples
{
    public class _TaskWaitAny : IBaseExecutor
    {
        public void Run()
        {
            Task taskA = Task.Factory.StartNew(() =>
            {
                Task.Delay(2000).Wait();
                Console.WriteLine("WaitAny | TaskA finished");
            });

            Task taskB = Task.Factory.StartNew(() => Console.WriteLine("WaitAny | TaskB finished"));

            Task taskC = Task.Factory.StartNew(() =>
            {
                Task.Delay(1000).Wait();
                Console.WriteLine("WaitAny | TaskC finished");
            });

            Task.WaitAny(taskA, taskB, taskC);
            Console.WriteLine("WaitAny | Calling method finishes");
        }
    }
}
