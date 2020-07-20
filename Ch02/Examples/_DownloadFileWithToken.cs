using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Ch02.Examples
{
    public class _DownloadFileWithToken : IBaseExecutor
    {
        public void Run()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;
            DownloadFileWithToken(token);
            //Random delay before we cancel token
            Task.Delay(2000);
            cancellationTokenSource.Cancel();
            Console.ReadLine();
        }

        private static void DownloadFileWithToken(CancellationToken token)
        {
            WebClient webClient = new WebClient();
            //Here we are registering callback delegate that will get called
            //as soon as user cancels token
            token.Register(() => webClient.CancelAsync());
            webClient.DownloadStringAsync(new Uri("http://www.google.com"));
            webClient.DownloadStringCompleted += (sender, e) =>
            {
                //Wait for 3 seconds so we have enough time to cancel task
                Task.Delay(3000);
                if (!e.Cancelled)
                    Console.WriteLine("Download Complete.");
                else
                    Console.WriteLine("Download Cancelled.");
            };

        }
    }
}
