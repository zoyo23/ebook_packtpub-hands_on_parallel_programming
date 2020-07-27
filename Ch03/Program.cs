using Ch03.Examples;
using System;
using System.Threading.Tasks;

namespace Ch03
{
    class Program
    {
        private static readonly TimeSpan WaitingTimeAfterExecution = TimeSpan.FromSeconds(5);

        static void Main(string[] args)
        {
            //new _ParallelLoop().Run();

            //new _PartitionStrategy().Run();

            //new _ParallelLoopCanceling().Run();

            new _ThreadStorage().Run();

            Task.Delay(WaitingTimeAfterExecution).Wait();
        }
    }
}
