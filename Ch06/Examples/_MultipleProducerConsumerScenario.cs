using Common.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ch06.Examples
{
    public class _MultipleProducerConsumerScenario : IBaseExecutor
    {
        public void Run()
        {
            //MultipleProducerConsumerScenarioUsingBlockingCollection();
            MultipleProducerConsumerScenarioUsingConcurrentDictionary();
        }

        private void MultipleProducerConsumerScenarioUsingConcurrentDictionary()
        {
            ConcurrentDictionary<int, string> concurrentDictionary = new ConcurrentDictionary<int, string>();

            Task producerTask1 = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(100);
                    concurrentDictionary.TryAdd(i, (i * i).ToString());
                }
            });

            Task producerTask2 = Task.Factory.StartNew(() =>
            {
                for (int i = 10; i < 25; i++)
                {
                    concurrentDictionary.TryAdd(i, (i * i).ToString());
                }
            });

            Task producerTask3 = Task.Factory.StartNew(() =>
            {
                for (int i = 15; i < 20; i++)
                {
                    Thread.Sleep(100);
                    concurrentDictionary.AddOrUpdate(i, (i * i).ToString(), (key, value) => (key * key).ToString());
                }
            });

            Task.WaitAll(producerTask1, producerTask2);
            Console.WriteLine("Keys are {0} ", string.Join(",", concurrentDictionary.Keys.Select(c => c.ToString()).ToArray()));
        }

        private void MultipleProducerConsumerScenarioUsingBlockingCollection()
        {
            BlockingCollection<int>[] produceCollections = new BlockingCollection<int>[2];
            produceCollections[0] = new BlockingCollection<int>(5);
            produceCollections[1] = new BlockingCollection<int>(5);

            Task producerTask1 = Task.Factory.StartNew(() =>
            {
                for (int i = 1; i <= 5; ++i)
                {
                    produceCollections[0].Add(i);
                    Thread.Sleep(200);
                }
                produceCollections[0].CompleteAdding();
            });

            Task producerTask2 = Task.Factory.StartNew(() =>
            {
                for (int i = 6; i <= 10; ++i)
                {
                    produceCollections[1].Add(i);
                    Thread.Sleep(400);
                }
                produceCollections[1].CompleteAdding();
            });


            while (!produceCollections[0].IsCompleted || !produceCollections[1].IsCompleted)
            {
                int item;
                BlockingCollection<int>.TryTakeFromAny(produceCollections, out item, TimeSpan.FromSeconds(1));

                if (item != default(int))
                {
                    Console.WriteLine($"Item fetched is {item}");
                }
            }
        }
    }
}
