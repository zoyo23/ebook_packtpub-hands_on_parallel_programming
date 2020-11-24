using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ch05.Examples
{
    public class _LightweightSynchronizationPrimitives : IBaseExecutor
    {
        public void Run()
        {
            //ReaderWriterLockSlimExample();
            //SemaphoreSlimExample();
            //ManualResetEventSlimExample();
            //BarrierAndCountdownEventExample();
            SpinLockExample();
        }

        #region SpinLock
        static SpinLock _spinLock = new SpinLock();
        static List<int> _itemsList = new List<int>();
        private void SpinLockExample()
        {
            Parallel.For(0, 10, (i) => SpinLock(i));
        }
        private static void SpinLock(int number)
        {
            bool lockTaken = false;
            try
            {
                Console.WriteLine($"{number} | Task {Task.CurrentId} Waiting for lock");
                _spinLock.Enter(ref lockTaken);
                Console.WriteLine($"{number} | Task {Task.CurrentId} Updating list");
                _itemsList.Add(number);
            }
            finally
            {
                if (lockTaken)
                {
                    Console.WriteLine($"{number} | Task {Task.CurrentId} Exiting Update");

                    _spinLock.Exit(false);
                }
            }
        }
        #endregion

        #region ManualResetEventSlim
        ManualResetEventSlim manualResetEvent = new ManualResetEventSlim(false);
        private void ManualResetEventSlimExample()
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
                    manualResetEvent.Wait();
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

        #region SemaphoreSlim
        private void SemaphoreSlimExample()
        {
            var range = Enumerable.Range(1, 120);
            SemaphoreSlim semaphore = new SemaphoreSlim(5, 10);
            range.AsParallel().AsOrdered().ForAll(i =>
            {
                try
                {
                    semaphore.Wait();
                    Console.WriteLine($"Index {i} making service call using Task { Task.CurrentId}");
                    //Simulate Http call
                    CallService(i);
                    Console.WriteLine($"Index {i} releasing semaphore using Task { Task.CurrentId}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Index {i} Catch a '{e.GetType().Name}' using Task { Task.CurrentId}");
                }
                finally
                {
                    semaphore.Release();
                }
            });

        }
        private static void CallService(int i)
        {
            Thread.Sleep(1000);
            if (i == 5)
            {
                throw new ValidationException("Ops, something went wrong.");
            }
        }

        #endregion

        #region ReaderWriterLockSlim
        static ReaderWriterLockSlim _readerWriterLockSlim = new ReaderWriterLockSlim();
        static List<int> _list = new List<int>();
        private void ReaderWriterLockSlimExample()
        {
            Task writerTask = Task.Factory.StartNew(WriterTask);
            for (int i = 0; i < 3; i++)
            {
                Task readerTask = Task.Factory.StartNew(ReaderTask);
            }
        }
        static void WriterTask()
        {
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    _readerWriterLockSlim.EnterWriteLock();
                    Console.WriteLine($"Entered WriteLock on Task { Task.CurrentId} ");
                    int random = new Random().Next(1, 10);
                    _list.Add(random);
                    Console.WriteLine($"Added {random} to list on Task { Task.CurrentId} ");
                    Console.WriteLine($"Exiting WriteLock on Task { Task.CurrentId} ");
                }
                finally
                {
                    _readerWriterLockSlim.ExitWriteLock();
                }
                Thread.Sleep(1000);
            }
        }
        static void ReaderTask()
        {
            for (int i = 0; i < 2; i++)
            {
                _readerWriterLockSlim.EnterReadLock();
                Console.WriteLine($"Entered ReadLock on Task {Task.CurrentId}");
                Console.WriteLine($"Items: {_list.Select(j => j.ToString()).Aggregate((a, b) => a + "," + b)} on Task { Task.CurrentId} ");
                Console.WriteLine($"Exiting ReadLock on Task {Task.CurrentId}");
                _readerWriterLockSlim.ExitReadLock();
                Thread.Sleep(1000);
            }
        }
        #endregion

        #region Barrier And CountdownEvent
        static string _serviceName = string.Empty;
        static Barrier serviceBarrier = new Barrier(5);
        static CountdownEvent serviceHost1CountdownEvent = new CountdownEvent(6);
        static CountdownEvent serviceHost2CountdownEvent = new CountdownEvent(6);
        static CountdownEvent finishCountdownEvent = new CountdownEvent(5);
        private void BarrierAndCountdownEventExample()
        {
            Task[] tasks = new Task[5];

            Task serviceManager = Task.Factory.StartNew(() =>
            {
                //Block until service name is set by any of thread
                while (string.IsNullOrEmpty(_serviceName))
                {
                    Console.WriteLine($"Waitting serviceName be setted.");
                    Thread.Sleep(1000);
                }

                string serviceName = _serviceName;
                HostService(serviceName);

                //Now signal other threads to proceed making calls to service1
                serviceHost1CountdownEvent.Signal();

                //Wait for worker tasks to finish service1 calls 
                serviceHost1CountdownEvent.Wait();

                //Block until service name is set by any of thread
                while (_serviceName != "Service2")
                {
                    Console.WriteLine($"Waitting serviceName not be setted to 'Service2'.");
                    Thread.Sleep(1000);
                }

                Console.WriteLine($"All tasks completed for service { serviceName}.");

                //Close current service and start the other service
                CloseService(serviceName);
                HostService(_serviceName);

                //Now signal other threads to proceed making calls to service2
                serviceHost2CountdownEvent.Signal();
                serviceHost2CountdownEvent.Wait();

                //Wait for worker tasks to finish service2 calls
                finishCountdownEvent.Wait();
                CloseService(_serviceName);
                Console.WriteLine($"All tasks completed for service { _serviceName}.");
            });

            for (int i = 0; i < 5; ++i)
            {
                int j = i;
                tasks[j] = Task.Factory.StartNew(() =>
                {
                    GetDataFromService1And2(j);
                });
            }

            Task.WaitAll(tasks);
            Console.WriteLine("Fetch completed");
        }
        private static void GetDataFromService1And2(int j)
        {
            _serviceName = "Service1";
            serviceHost1CountdownEvent.Signal();
            Console.WriteLine($"Task with id {Task.CurrentId} signalled countdown event and waiting for service to start");

            //Waiting for service to start
            serviceHost1CountdownEvent.Wait();
            Console.WriteLine($"Task with id {Task.CurrentId} fetching data from service ");
            serviceBarrier.SignalAndWait();


            //change servicename
            _serviceName = "Service2";

            //Signal Countdown event
            serviceHost2CountdownEvent.Signal();
            Console.WriteLine($"Task with id {Task.CurrentId} signalled countdown event and waiting for service to start");
            serviceHost2CountdownEvent.Wait();
            Console.WriteLine($"Task with id {Task.CurrentId} fetching data from service ");
            serviceBarrier.SignalAndWait();

            //Signal Countdown event
            finishCountdownEvent.Signal();
        }
        private static void HostService(string name)
        {
            Console.WriteLine($"Service {name} hosted");
        }
        private static void CloseService(string name)
        {
            Console.WriteLine($"Service {name} closed");
        }
        #endregion
    }
}
