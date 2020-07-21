using System;
using System.Threading;

namespace Ch01
{
    public class _2ThreadStart : IBaseExecutor
    {
        public void Run()
        {
            Console.WriteLine("Start Execution!!!");

            //Using Thread without parameter
            CreateThreadUsingThreadClassWithoutParameter();

            Console.WriteLine("Finish Execution");
        }

        private static void CreateThreadUsingThreadClassWithoutParameter()
        {
            Thread thread;
            thread = new Thread(new
            ThreadStart(PrintNumber10Times)); thread.Start();
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
