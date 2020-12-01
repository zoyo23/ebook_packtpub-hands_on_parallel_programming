using Ch07.Domain;
using Common.Interfaces;
using System;

namespace Ch07.Examples
{
    public class _ExceptionsWithLazyWithCaching : IBaseExecutor
    {
        public void Run()
        {
            ExceptionsWithLazyWithCachingExample();
            //ExceptionsWithLazyWitouthCachingExample();
        }

        private void ExceptionsWithLazyWithCachingExample()
        {
            Console.WriteLine("Creating Lazy object");
            Func<Data> dataFetchLogic = new Func<Data>(() => GetDataFromDatabase());
            Lazy<Data> lazyDataWrapper = new Lazy<Data>(dataFetchLogic);
            Console.WriteLine("Lazy Object Created");
            Console.WriteLine("Now we want to access data");

            AccessData(lazyDataWrapper);
        }

        private void ExceptionsWithLazyWitouthCachingExample()
        {
            Console.WriteLine("Creating Lazy object");
            Func<Data> dataFetchLogic = new Func<Data>(() => GetDataFromDatabase());
            Lazy<Data> lazyDataWrapper = new Lazy<Data>(dataFetchLogic, System.Threading.LazyThreadSafetyMode.PublicationOnly);
            Console.WriteLine("Lazy Object Created");
            Console.WriteLine("Now we want to access data");

            AccessData(lazyDataWrapper);
        }

        static int counter = 0;
        public Data CachedData { get; set; }
        static Data GetDataFromDatabase()
        {
            if (counter == 0)
            {
                Console.WriteLine("Throwing exception");
                throw new Exception("Some Error has occurred");
            }
            else
            {
                return new Data();
            }
        }
        static void AccessData(Lazy<Data> lazyDataWrapper)
        {
            Data data = null;

            try
            {
                Console.WriteLine("Get Value 1");
                data = lazyDataWrapper.Value;
                Console.WriteLine("Data Fetched on Attempt 1");
            }
            catch (Exception)
            {
                Console.WriteLine("Exception 1");
            }

            try
            {
                counter++;
                Console.WriteLine("Get Value 2");
                data = lazyDataWrapper.Value;
                Console.WriteLine("Data Fetched on Attempt 2");
            }
            catch (Exception)
            {
                Console.WriteLine("Exception 2");
            }

            Console.WriteLine("Finishing up");
        }
    }
}
