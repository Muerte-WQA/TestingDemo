using System.Diagnostics;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestingDemo.Core;

namespace TestingDemo.PerformanceTests
{
    [TestClass]
    public class PerformanceTests
    {
        [TestMethod]
        public void SingleOperation_Under_50ms()
        {
            var calc = new Calculator();
            var sw = Stopwatch.StartNew();
            calc.Add(1, 1);
            sw.Stop();
            // В MSTest + .NET Framework замеры менее точные, поэтому порог выше
            sw.ElapsedMilliseconds.Should().BeLessThan(50);
        }

        [TestMethod]
        public void TenThousandOperations_Under_2Seconds()
        {
            var calc = new Calculator();
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 10_000; i++)
                calc.Add(i, i);
            sw.Stop();

            sw.ElapsedMilliseconds.Should().BeLessThan(2000);
            calc.GetHistory().Should().HaveCount(10_000);
        }

        [TestMethod]
        [Ignore] // тяжёлый тест, включать вручную
        public void Stress_100kOperations()
        {
            var calc = new Calculator();
            for (int i = 0; i < 100_000; i++)
                calc.Multiply(i, 2);
            calc.GetHistory().Should().HaveCount(100_000);
        }
    }
}