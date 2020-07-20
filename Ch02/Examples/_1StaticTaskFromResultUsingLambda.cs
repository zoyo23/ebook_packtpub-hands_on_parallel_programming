using Ch02;
using System;
using System.Threading.Tasks;

namespace Ch02.Examples
{
    public class _1StaticTaskFromResultUsingLambda : IBaseExecutor
    {
        public void Run()
        {
            StaticTaskFromResultUsingLambda();
        }

        private static void StaticTaskFromResultUsingLambda()
        {
            Task<int> resultTask = Task.FromResult<int>(Sum(10));
            Console.WriteLine(resultTask.Result);
        }
        private static int Sum(int n)
        {
            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += i;
            }
            return sum;
        }
    }
}
