using System.Linq;

namespace Ch04.Examples
{
    public class _CombiningParallelAndSequential : IBaseExecutor
    {
        public void Run()
        {
            CombiningParallelAndSequential();
        }

        private void CombiningParallelAndSequential()
        {
            var range = Enumerable.Range(1, 1000);

            range.AsParallel().Where(i => i % 2 == 0)
                    .AsSequential().Where(i => i % 8 == 0)
                    .AsParallel().OrderBy(i => i);

        }
    }
}
