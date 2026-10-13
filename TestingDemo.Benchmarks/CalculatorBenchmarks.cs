using BenchmarkDotNet.Attributes;
using TestingDemo.Core;

namespace TestingDemo.Benchmarks
{
    [MemoryDiagnoser]
    public class CalculatorBenchmarks
    {
        private Calculator _calc;

        [GlobalSetup]
        public void Setup()
        {
            _calc = new Calculator();
        }

        [Benchmark]
        public double AddTwoNumbers() => _calc.Add(3.14, 2.71);

        [Benchmark]
        public double PowerOperation() => _calc.Power(2, 16);

        [Benchmark]
        public int HistoryAccumulation()
        {
            var c = new Calculator();
            for (int i = 0; i < 1000; i++) c.Add(i, i);
            return c.GetHistory().Count;
        }
    }
}