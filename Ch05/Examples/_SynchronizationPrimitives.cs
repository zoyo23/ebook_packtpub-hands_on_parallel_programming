using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ch05.Examples
{
    public class _SynchronizationPrimitives : IBaseExecutor
    {
        public void Run()
        {
            IncrementeWithoutInterlockedOperations();

            IncrementeWithInterlockedOperations();

            ListInterlockedOperations();
        }

        private void ListInterlockedOperations()
        {
            long _counter = 0;
            Console.WriteLine($"ListInterlockedOperations | Counter: { _counter }");

            //_counter becomes 1
            Interlocked.Increment(ref _counter);
            Console.WriteLine($"ListInterlockedOperations | Interlocked.Increment | Counter: { _counter }");

            // _counter becomes 0
            Interlocked.Decrement(ref _counter);
            Console.WriteLine($"ListInterlockedOperations | Interlocked.Decrement | Counter: { _counter }");

            // Add: _counter becomes 2
            Interlocked.Add(ref _counter, 2);
            Console.WriteLine($"ListInterlockedOperations | Interlocked.Add | Counter: { _counter }");

            //Subtract: _counter becomes 0
            Interlocked.Add(ref _counter, -2);
            Console.WriteLine($"ListInterlockedOperations | Interlocked.Add | Counter: { _counter }");

            // Reads 64 bit field
            Console.WriteLine(Interlocked.Read(ref _counter));
            Console.WriteLine($"ListInterlockedOperations | Interlocked.Read | Counter: { _counter }");

            // Swaps _counter value with 10
            Console.WriteLine(Interlocked.Exchange(ref _counter, 10));
            Console.WriteLine($"ListInterlockedOperations | Interlocked.Exchange | Counter: { _counter }");

            //Checks if _counter is 10 and if yes replace with 100
            Console.WriteLine(Interlocked.CompareExchange(ref _counter, 100, 10));
            Console.WriteLine($"ListInterlockedOperations | Interlocked.CompareExchange | Counter: { _counter }");
        }

        private void IncrementeWithInterlockedOperations()
        {
            int _counter = 0;

            Parallel.For(1, 1000, i =>
            {
                Thread.Sleep(100);
                Interlocked.Increment(ref _counter);
            });

            Console.WriteLine($"IncrementeWithInterlockedOperations | Value for counter should be 999 and is { _counter }");
        }

        private void IncrementeWithoutInterlockedOperations()
        {
            int _counter = 0;

            Parallel.For(1, 1000, i =>
            {
                Thread.Sleep(100);
                _counter++;
            });

            Console.WriteLine($"IncrementeWithoutInterlockedOperations | Value for counter should be 999 and is { _counter }");
        }
    }
}
