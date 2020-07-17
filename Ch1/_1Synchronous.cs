using System;

namespace Ch1
{
    public static class _1Synchronous
    {
        public static void Executar()
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
