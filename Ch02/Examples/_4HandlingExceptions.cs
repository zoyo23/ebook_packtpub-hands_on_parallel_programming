using System;
using System.Threading.Tasks;

namespace Ch02.Examples
{
    public class _4HandlingExceptions : IBaseExecutor
    {
        public void Run()
        {
            //HandleSingleTask();

            //HandleMultiplesTasks();

            HandleMultiplesTasksWithCallBack();
        }

        private void HandleMultiplesTasksWithCallBack()
        {
            Task taskA = Task.Factory.StartNew(() => throw new DivideByZeroException());
            Task taskB = Task.Factory.StartNew(() =>
            {
                Task.Delay(2000).Wait();
                throw new ArithmeticException();
            });
            Task taskC = Task.Factory.StartNew(() => throw new NullReferenceException());

            try
            {
                Task.WaitAll(taskA, taskB, taskC);
            }
            catch (AggregateException ex)
            {
                ex.Handle(innerException =>
                {
                    Console.WriteLine(innerException.Message);
                    return true;
                });
            }
        }

        private void HandleMultiplesTasks()
        {
            Task taskA = Task.Factory.StartNew(() => throw new DivideByZeroException());
            Task taskB = Task.Factory.StartNew(() =>
            {
                Task.Delay(2000).Wait();
                throw new ArithmeticException();
            });
            Task taskC = Task.Factory.StartNew(() => throw new NullReferenceException());

            try
            {
                Task.WaitAll(taskA, taskB, taskC);
            }
            catch (AggregateException ex)
            {
                foreach (Exception innerException in ex.InnerExceptions)
                {
                    Console.WriteLine(innerException.Message);
                }
            }
        }

        private static void HandleSingleTask()
        {
            Task task = null;
            try
            {
                task = Task.Factory.StartNew(() =>
                {
                    int num = 0, num2 = 25;
                    var result = num2 / num;
                });

                task.Wait();

            }
            catch (AggregateException ex)
            {
                Console.WriteLine($"Task has finished with exception { ex.InnerException.Message}");
            }
        }
    }
}
