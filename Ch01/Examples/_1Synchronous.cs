using Common.Interfaces;
using System;

namespace Ch01
{
    public class _1Synchronous : IBaseExecutor
    {

        public void Run()
        {
            Console.WriteLine("Start Execution!!!");

            PrintNumber10Times();

            Console.WriteLine("Finish Execution");
        }
        private static void PrintNumber10Times()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write(1);
            }
            Console.WriteLine();
        }

    }
}
