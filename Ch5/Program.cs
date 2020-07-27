using Ch5.Examples;
using System;
using System.Threading.Tasks;

namespace Ch5
{
    class Program
    {
        private static readonly TimeSpan WaitingTimeAfterExecution = TimeSpan.FromSeconds(5);

        static void Main(string[] args)
        {
            new _SynchronizationPrimitives().Run();

            Task.Delay(WaitingTimeAfterExecution).Wait();
        }
    }
}
