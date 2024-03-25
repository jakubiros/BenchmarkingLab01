using BenchmarkDotNet.Running;

namespace BenchmarkingLab01
{
    public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run(typeof(SortingAlgorithms));
        }
    }
}
