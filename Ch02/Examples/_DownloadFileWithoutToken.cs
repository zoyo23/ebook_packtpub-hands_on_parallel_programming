using Common.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Ch02.Examples
{
    public class _DownloadFileWithoutToken : IBaseExecutor
    {
        public void Run()
        {
            DownloadFileWithoutToken();
            //Random delay before we cancel token
            Task.Delay(2000);
            Console.ReadLine();
        }

        private static void DownloadFileWithoutToken()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringAsync(new Uri("http://www.google.com"));
            webClient.DownloadStringCompleted += (sender, e) =>
            {
                if (!e.Cancelled)
                    Console.WriteLine("Download Complete.");
                else
                    Console.WriteLine("Download Cancelled.");
            };
        }
    }
}
