using Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ch07.Examples
{
    public class _LazyInitializationWithThreadLocalStorage : IBaseExecutor
    {
        public void Run()
        {
            //WithoutThreadLocalStorage();
            WithThreadLocalStorage();
        }

        #region Without Thread-Local Storage
        [ThreadStatic]
        static int counterWithout = 5;
        void WithoutThreadLocalStorage()
        {
            for (int i = 0; i < 15; i++)
            {
                Task.Factory.StartNew(() => Console.WriteLine(counterWithout));
            }
            Console.ReadLine();
        }
        #endregion

        #region With Thread-Local Storage
        static ThreadLocal<int> counterWith = new ThreadLocal<int>(() => 5);
        public static void WithThreadLocalStorage()
        {
            for (int i = 0; i < 15; i++)
            {
                Task.Factory.StartNew(() => Console.WriteLine($"Thread with id {Task.CurrentId} has counter value as {counterWith.Value}"));
            }
            Console.ReadLine();
        }
        #endregion
    }
}
