using Common.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Ch03.Examples
{
    public class _PartitionStrategy : IBaseExecutor
    {
        public void Run()
        {
            ParallelWithChunkPartition();
        }

        private void ParallelWithChunkPartition()
        {
            OrderablePartitioner<Tuple<int, int>> orderablePartitioner = Partitioner.Create(0, 100);

            Parallel.ForEach(orderablePartitioner, (range, state) =>
            {
                var startIndex = range.Item1;
                var endIndex = range.Item2;
                Console.WriteLine($"Range execution finished on task {Task.CurrentId} with range {startIndex} - {endIndex} ");
            });
        }
    }
}
