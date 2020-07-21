using System;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace Ch01
{
    public class _5BackgroundWorker : IBaseExecutor
    {
        public void Run()
        {
            var backgroundWorker = new BackgroundWorker();

            //In order to raise progress reporting and cancellation events, you need to set the following properties to true
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;

            //You need to subscribe to the ProgressChanged event to receive progress, 
            //the DoWork event to pass a method that needs to be invoked by the thread, 
            //and the RunWorkerCompleted event to receive either the final results or any error messages from the thread's execution
            backgroundWorker.DoWork += SimulateServiceCall;
            backgroundWorker.ProgressChanged += ProgressChanged;
            backgroundWorker.RunWorkerCompleted += RunWorkerCompleted;

            //Once this has been set up, you can invoke the worker by calling the following command
            backgroundWorker.RunWorkerAsync();

            Console.WriteLine("To Cancel Worker Thread Press C.");

            while (backgroundWorker.IsBusy)
            {
                if (Console.ReadKey(true).KeyChar.ToString().ToUpper().Equals("C"))
                {
                    backgroundWorker.CancelAsync();
                }
            }
        }

        // This method executes when the background worker finishes
        // execution
        private static void RunWorkerCompleted(object sender,
                                               RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Console.WriteLine(e.Error.Message);
            }
            else
            {
                Console.WriteLine($"Result from service call is { e.Result }");
            }
        }

        // This method is called when background worker want to
        // report progress to caller
        private static void ProgressChanged(object sender,
                                            ProgressChangedEventArgs e)
        {
            Console.WriteLine($"{e.ProgressPercentage}% completed");
        }

        // Service call we are trying to simulate
        private static void SimulateServiceCall(object sender,
                                                DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            StringBuilder data = new StringBuilder();
            //Simulate a streaming service call which gets data and
            //store it to return back to caller
            for (int i = 1; i <= 100; i++)
            {
                //worker.CancellationPending will be true if user
                //press C
                if (!worker.CancellationPending)
                {
                    data.Append(i);
                    worker.ReportProgress(i);
                    Thread.Sleep(100);
                    //Try to uncomment and throw error
                    //throw new Exception("Some Error has occurred");
                }
                else
                {
                    //Cancels the execution of worker
                    worker.CancelAsync();
                }
            }

            //If there are no exceptions, the result of the thread's execution can be returned to the caller by setting the following
            e.Result = data;
        }
    }
}
