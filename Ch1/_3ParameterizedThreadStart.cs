using System;
using System.Threading;

namespace Ch1
{
    public static class _3ParameterizedThreadStart
    {
        public static void Executar()
        {
            Console.WriteLine("Start Execution!!!");

            //Using Thread with parameter
            CreateThreadUsingThreadClassWithParameter();

            Console.WriteLine("Finish Execution");
        }
        private static void CreateThreadUsingThreadClassWithParameter()
        {
            Thread thread;
            thread = new Thread(new
            ParameterizedThreadStart(PrintNumberNTimes));
            thread.Start(10);
        }
        private static void PrintNumberNTimes(object times)
        {
            int n = Convert.ToInt32(times);
            for (int i = 0; i < n; i++)
            {
                Console.Write(1);
            }
            Console.WriteLine();
        }
    }
}
