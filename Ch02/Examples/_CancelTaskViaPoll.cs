using Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ch02.Examples
{
    public class _CancelTaskViaPoll : IBaseExecutor
    {
        public void Run()
        {
            CancelTaskViaPoll();
        }

        private static void CancelTaskViaPoll()
        {
            CancellationTokenSource cancellationTokenSource =
            new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            var sumTaskViaTaskOfInt = new Task(() => LongRunningSum(token), token);

            sumTaskViaTaskOfInt.Start();

            //Wait for user to press key to cancel task
            Console.ReadLine();
            cancellationTokenSource.Cancel();
        }
        private static void LongRunningSum(CancellationToken token)
        {
            for (int i = 0; i < 1000; i++)
            {
                //Simulate long running operation
                Task.Delay(10000);
                Console.WriteLine(i);
                if (token.IsCancellationRequested)
                    token.ThrowIfCancellationRequested();
            }
        }
    }
}
