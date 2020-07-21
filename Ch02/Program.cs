using Ch02.Examples;
using System;
using System.Threading.Tasks;

namespace Ch02
{
    class Program
    {
        private static readonly TimeSpan WaitingTimeAfterExecution = TimeSpan.FromSeconds(5);

        static void Main(string[] args)
        {
            //new _1StaticTaskFromResultUsingLambda().Run();

            //new _2GettingResultFromTasks().Run();

            //new _4HandlingExceptions().Run();

            //new _CancelTaskViaPoll().Run();

            //new _DownloadFileWithoutToken().Run();

            //new _DownloadFileWithToken().Run();

            //new _TaskWait().Run();

            //new _ReadFile().Run();

            //new _TaskContinue().Run();

            //new _TaskChild().Run();

            Task.Delay(WaitingTimeAfterExecution).Wait();
        }
    }
}
