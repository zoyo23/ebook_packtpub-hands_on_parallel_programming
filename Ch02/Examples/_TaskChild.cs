using Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Ch02.Examples
{
    public class _TaskChild : IBaseExecutor
    {
        public void Run()
        {
            //CreatingDetachedTask();

            CreatingAttachedTask();
        }

        private void CreatingAttachedTask()
        {
            Task parentTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent task started");

                Task childTask = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Child task started");
                },
                TaskCreationOptions.AttachedToParent);

                Console.WriteLine("Parent task Finish");
            });
            //Wait for parent to finish
            parentTask.Wait();
            Console.WriteLine("Work Finished");

        }

        private void CreatingDetachedTask()
        {
            Task parentTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine(" Parent task started");

                Task childTask = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine(" Child task started");
                });

                Console.WriteLine(" Parent task Finish");
            });

            //Wait for parent to finish
            parentTask.Wait();
            Console.WriteLine("Work Finished");
        }
    }
}
