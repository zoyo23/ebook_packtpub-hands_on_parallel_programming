using System;
using System.Threading.Tasks;

namespace Ch02.Examples
{
    public class _TaskWait : IBaseExecutor
    {
        public void Run()
        {
            TaskWaitAll();

            TaskWaitAny();

            TaskWhenAll();

            TaskWhenAny();
        }

        private void TaskWhenAny()
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

        private void TaskWhenAll()
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

        private void TaskWaitAny()
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

        private void TaskWaitAll()
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
