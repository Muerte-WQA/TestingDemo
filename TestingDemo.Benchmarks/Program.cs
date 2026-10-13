using BenchmarkDotNet.Running;
using TestingDemo.Benchmarks;

namespace TestingDemo.Benchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<CalculatorBenchmarks>();
        }
    }
}