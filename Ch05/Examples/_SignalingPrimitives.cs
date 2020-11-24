using Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ch05.Examples
{
    public class _SignalingPrimitives : IBaseExecutor
    {
        public void Run()
        {
            //ThreadJoinExample();
            //AutoResetEventExample();
            ManualResetEventExample();
        }

        #region ManualResetEvent
        ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        private void ManualResetEventExample()
        {
            #region Connection OFF
            Task signalOffTask = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Network is down");
                    manualResetEvent.Reset();
                }
            });
            #endregion

            #region Connection ON
            Task signalOnTask = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(5000);
                    Console.WriteLine("Network is Up");
                    manualResetEvent.Set();
                }
            });
            #endregion

            for (int i = 0; i < 3; i++)
            {
                Parallel.For(0, 5, (j) =>
                {
                    Console.WriteLine($"Task with id {Task.CurrentId} waiting for network to be up");
                    manualResetEvent.WaitOne();
                    Console.WriteLine($"Task with id {Task.CurrentId} making service call");

                    DummyService(j);
                });
                Thread.Sleep(2000);
            }

        }

        private static void DummyService(int i)
        {
            var rand = new Random().Next(1000, 5000);
            Console.WriteLine($"Index {i} waiting for: {rand} ms, using Task {Task.CurrentId}");
            Thread.Sleep(rand);
        }
        #endregion

        #region AutoResetEvent
        //If we set the initial state to signaled, or true, the first thread will go through while the others wait for a signal
        AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        private void AutoResetEventExample()
        {
            Task signallingTask = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    autoResetEvent.Set();
                }
            });

            int sum = 0;

            Parallel.For(1, 10, (i) =>
            {
                Console.WriteLine($"Task with id {Task.CurrentId} waiting for signal to enter | {i}");
                autoResetEvent.WaitOne();
                Console.WriteLine($"Task with id {Task.CurrentId} received signal to enter | {i}");

                sum += i;
            });
        }
        #endregion

        #region Thread.Join
        private void ThreadJoinExample()
        {
            int result = 0;
            Thread childThread = new Thread(() =>
            {
                Thread.Sleep(5000);
                result = 10;
            });
            childThread.Start();
            childThread.Join();
            Console.WriteLine($"Result is {result}");

        }
        #endregion
    }
}
