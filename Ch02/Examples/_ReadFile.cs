using Common.Interfaces;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Ch02.Examples
{
    public class _ReadFile : IBaseExecutor
    {
        private static readonly int BufferSize = 1024 * 1024;
        public void Run()
        {
            //ReadFileSynchronously();

            //ReadFileUsingAPMAsyncWithoutCallback();

            ReadFileUsingTask();
        }

        private void ReadFileUsingTask()
        {
            string filePath = @"Test.txt";
            //Open the stream and read content.
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize, FileOptions.Asynchronous))
            {
                byte[] buffer = new byte[BufferSize];
                UTF8Encoding encoder = new UTF8Encoding(true);
                //Start task that will read file asynchronously
                var task = Task<int>.Factory.FromAsync(fs.BeginRead, fs.EndRead, buffer, 0, buffer.Length, null);
                Console.WriteLine("Do Something while file is read  asynchronously");
                //Wait for task to finish
                task.Wait();
                Console.WriteLine(encoder.GetString(buffer));
            }

        }

        private void ReadFileUsingAPMAsyncWithoutCallback()
        {
            string filePath = @"Test.txt";
            //Open the stream and read content.
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize, FileOptions.Asynchronous))
            {
                byte[] buffer = new byte[BufferSize];
                UTF8Encoding encoder = new UTF8Encoding(true);
                IAsyncResult result = fs.BeginRead(buffer, 0, buffer.Length, null, null);
                Console.WriteLine("Do Something here");
                int numBytes = fs.EndRead(result);
                fs.Close();
                Console.WriteLine(encoder.GetString(buffer));
            }
        }

        private static void ReadFileSynchronously()
        {
            string path = @"Test.txt";
            //Open the stream and read content.
            using (FileStream fs = File.OpenRead(path))
            {
                byte[] b = new byte[BufferSize];
                UTF8Encoding encoder = new UTF8Encoding(true);
                fs.Read(b, 0, b.Length);
                Console.WriteLine(encoder.GetString(b));
            }
        }
    }
}
