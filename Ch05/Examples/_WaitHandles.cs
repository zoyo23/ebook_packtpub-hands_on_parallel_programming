using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Ch05.Examples
{
    public class _WaitHandles : IBaseExecutor
    {
        public void Run()
        {
            //WaitAllExample();
            WaitAnyExample();
        }

        #region WaitAny
        static int findIndex = -1;
        static string winnerAlgo = string.Empty;

        private void WaitAnyExample()
        {
            WaitHandle[] waitHandles = new WaitHandle[]
            {
                new AutoResetEvent(false),
                new AutoResetEvent(false)
            };
            int itemToSearch = 20;
            var range = Enumerable.Range(0, 50).ToArray();

            ThreadPool.QueueUserWorkItem(
                new WaitCallback(LinearSearch),
                new
                {
                    Range = range,
                    ItemToFind = itemToSearch,
                    WaitHandle = waitHandles[0]
                });

            ThreadPool.QueueUserWorkItem(
                new WaitCallback(BinarySearch),
                new
                {
                    Range = range,
                    ItemToFind = itemToSearch,
                    WaitHandle = waitHandles[1]
                });

            WaitHandle.WaitAny(waitHandles);

            Console.WriteLine($"Item found at index {findIndex} and faster algo is { winnerAlgo }");
        }

        private static void BinarySearch(object state)
        {
            dynamic data = state;
            int[] x = data.Range;
            int valueToFind = data.ItemToFind;
            AutoResetEvent autoResetEvent = data.WaitHandle as AutoResetEvent;

            //Search for item using .NET framework built in Binary Search
            int foundIndex = Array.BinarySearch(x, valueToFind);

            //store the result globally
            Interlocked.CompareExchange(ref findIndex, foundIndex, -1);
            Interlocked.CompareExchange(ref winnerAlgo, "BinarySearch", string.Empty);

            //Signal event
            autoResetEvent.Set();
        }

        public static void LinearSearch(object state)
        {
            dynamic data = state;
            int[] x = data.Range;
            int valueToFind = data.ItemToFind;
            AutoResetEvent autoResetEvent = data.WaitHandle as AutoResetEvent;
            int foundIndex = -1;

            //Search for item linearly using for loop
            for (int i = 0; i < x.Length; i++)
            {
                if (valueToFind == x[i])
                {
                    foundIndex = i;
                    break;
                }
            }

            //store the result globally
            Interlocked.CompareExchange(ref findIndex, foundIndex, -1);
            Interlocked.CompareExchange(ref winnerAlgo, "LinearSearch", string.Empty);

            //Signal event
            autoResetEvent.Set();
        }
        #endregion

        #region WaitAll
        static int _dataFromService1 = 0;
        static int _dataFromService2 = 0;
        private void WaitAllExample()
        {
            List<WaitHandle> waitHandles = new List<WaitHandle>
            {
                new AutoResetEvent(false),
                new AutoResetEvent(false)
            };

            ThreadPool.QueueUserWorkItem(new WaitCallback(FetchDataFromService1), waitHandles.First());
            ThreadPool.QueueUserWorkItem(new WaitCallback(FetchDataFromService2), waitHandles.Last());
            //Waits for all the threads (waitHandles) to call the .Set()
            //method
            //i.e. wait for data to be returned from both service
            WaitHandle.WaitAll(waitHandles.ToArray());
            Console.WriteLine($"The Sum is { _dataFromService1 + _dataFromService2} ");
        }
        private static void FetchDataFromService1(object state)
        {
            Thread.Sleep(1000);
            _dataFromService1 = 890;
            var autoResetEvent = state as AutoResetEvent;
            autoResetEvent.Set();
            Console.WriteLine($"FetchDataFromService1 | Raise Event");
        }
        private static void FetchDataFromService2(object state)
        {
            Thread.Sleep(2000);
            _dataFromService2 = 3;
            var autoResetEvent = state as AutoResetEvent;
            autoResetEvent.Set();
            Console.WriteLine($"FetchDataFromService2 | Raise Event");
        }

        #endregion
    }
}
