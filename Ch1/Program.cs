using System;
using System.Threading;

namespace Ch1
{
    class Program
    {
        private static readonly TimeSpan WaitingTimeAfterExecution = TimeSpan.FromSeconds(5);
        static void Main(string[] args)
        {
            _1Synchronous.Executar();

            _2ThreadStart.Executar();

            _3ParameterizedThreadStart.Executar();

            Thread.Sleep(WaitingTimeAfterExecution);
        }
    }
}
