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
            //new _SynchronizationPrimitives().Run();
            new _Monitors().Run();
            //new _SignalingPrimitives().Run();
            //new _WaitHandles().Run();
            //new _LightweightSynchronizationPrimitives().Run();

            Task.Delay(WaitingTimeAfterExecution).Wait();
        }
    }
}
