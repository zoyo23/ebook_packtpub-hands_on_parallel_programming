using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace Ch03.Examples
{
    public class _ParallelLoop : IBaseExecutor
    {
        public void Run()
        {
            //ParallelInvoke();

            //ParallelFor();

            //ParallelForWithParallelOptions();

            //ParallelForEach();

            ParallelForEachWithParallelOptions();
        }

        private void ParallelForEachWithParallelOptions()
        {
            var items = Enumerable.Range(1, 100);
            Parallel.ForEach(items, new ParallelOptions { MaxDegreeOfParallelism = 4 },
            item =>
            {
                Console.WriteLine($"Index {item} executing on Task Id {Task.CurrentId}");
            });
        }

        private void ParallelForWithParallelOptions()
        {
            Parallel.For(1, 100, new ParallelOptions { MaxDegreeOfParallelism = 4 },
                index =>
                {
                    Console.WriteLine($"Index {index} executing on Task Id { Task.CurrentId} ");
                });
        }

        private void ParallelForEach()
        {
            List<string> urls = new List<string>() {
                "www.google.com" ,
                "www.yahoo.com",
                "www.bing.com"
            };

            Parallel.ForEach(urls, url =>
            {
                Ping pinger = new Ping();
                Console.WriteLine($"Ping Url {url} status is {pinger.Send(url).Status} by Task {Task.CurrentId} ");
            });
        }

        private void ParallelFor()
        {
            int totalFiles = 0;
            var files = Directory.GetFiles("C:\\");
            Parallel.For(0, files.Length, (i) =>
            {
                FileInfo fileInfo = new FileInfo(files[i]);
                if (fileInfo.CreationTime.Day == DateTime.Now.Day)
                    Interlocked.Increment(ref totalFiles);
            });
            Console.WriteLine($"Total number of files in C: drive are {files.Count()} and { totalFiles} files were created today.");
        }

        private void ParallelInvoke()
        {
            try
            {
                Parallel.Invoke(
                    () =>
                    {
                        Console.WriteLine("Action 1");
                        Task.Delay(1000).Wait();
                        Console.WriteLine("Another Action 1");

                    },
                    new Action(
                        () => Console.WriteLine("Action 2")
                    )
                );
            }
            catch (AggregateException aggregateException)
            {
                foreach (var ex in aggregateException.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("Unblocked");
        }
    }
}
