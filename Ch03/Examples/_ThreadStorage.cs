using Ch03;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ch03.Examples
{
    public class _ThreadStorage : IBaseExecutor
    {
        public void Run()
        {
            //ThreadLocalVariable();
            PartitionLocalVariable();
        }

        private void PartitionLocalVariable()
        {
            var numbers = Enumerable.Range(1, 60);

            long sumOfNumbers = 0;

            Action<long> taskFinishedMethod = (taskResult) =>
            {
                Console.WriteLine($"Sum at the end of all task iterations for task {Task.CurrentId} is {taskResult }");
                Interlocked.Add(ref sumOfNumbers, taskResult);
            };

            Parallel.ForEach<int, long>(numbers,
                                        () => 0, // method to initialize the local variable 
                                        (j, loop, subtotal) => // Action performed on each iteration 
                                        {
                                            subtotal += j; //Subtotal is Thread local variable
                                            return subtotal; // value to be passed to next iteration
                                        },
                                        taskFinishedMethod);

            Console.WriteLine($"The total of 60 numbers is {sumOfNumbers}");
        }

        private void ThreadLocalVariable()
        {
            var numbers = Enumerable.Range(1, 60);

            long sumOfNumbers = 0;

            Action<long> taskFinishedMethod = (taskResult) =>
            {
                Console.WriteLine($"Sum at the end of all task iterations for task {Task.CurrentId} is {taskResult }");
                Interlocked.Add(ref sumOfNumbers, taskResult);
            };

            Parallel.For(0, numbers.Count(), () => 0,
                        (j, loop, subtotal) =>
                        {
                            subtotal += j;
                            return subtotal;
                        },
                        taskFinishedMethod);

            Console.WriteLine($"The total of 60 numbers is {sumOfNumbers}");
        }
    }
}
