using Ch05.Examples;
using System;
using System.Threading.Tasks;

namespace Ch05
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
