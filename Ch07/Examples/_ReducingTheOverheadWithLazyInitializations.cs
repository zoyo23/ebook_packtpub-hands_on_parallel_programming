using Ch07.Domain;
using Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ch07.Examples
{
    public class _ReducingTheOverheadWithLazyInitializations : IBaseExecutor
    {
        public void Run()
        {
            SingleThreadEnsureInitialized();
            //MultipleThreadEnsureInitialized();
        }

        static Data _data;
        void SingleThreadEnsureInitialized()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Iteration {i}");
                Initializer();
            }
        }

        static bool _initialized;
        static object _locker = new object();
        void MultipleThreadEnsureInitialized()
        {
            Parallel.For(0, 10, (i) =>
            {
                //Console.WriteLine($"Iteration {i}");
                Initializer();
            });
        }

        private void Initializer()
        {
            Console.WriteLine($"Task with id {Task.CurrentId ?? 0}");

            LazyInitializer.EnsureInitialized(ref _data, ref _initialized, ref _locker, () =>
             {
                 Console.WriteLine($"Task with id {Task.CurrentId ?? 0} is Initializing data");
                 // Returns value that will be assigned in the ref parameter.
                 return new Data();
             });
        }
    }
}
