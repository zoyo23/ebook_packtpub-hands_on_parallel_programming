using ch03.Examples;
using System;
using System.Threading.Tasks;

namespace ch03
{
    class Program
    {
        private static readonly TimeSpan WaitingTimeAfterExecution = TimeSpan.FromSeconds(5);

        static void Main(string[] args)
        {
            //new _ParallelLoop().Run();

            //new _PartitionStrategy().Run();

            new _ParallelLoopCanceling().Run();

            Task.Delay(WaitingTimeAfterExecution).Wait();
        }
    }
}
