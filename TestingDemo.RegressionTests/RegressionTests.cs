using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestingDemo.Core;

namespace TestingDemo.RegressionTests
{
    [TestClass]
    public class RegressionTests
    {
        [TestMethod] // BUG-001
        public void Add_IntArguments_ReturnsCorrectSum()
        {
            var calc = new Calculator();
            calc.Add(2, 3).Should().Be(5);
        }

        [TestMethod] // BUG-002
        public void Divide_ByZero_ThrowsSpecificException()
        {
            var calc = new Calculator();
            System.Action act = () => calc.Divide(5, 0);
            act.Should().Throw<DivisionByZeroException>();
        }

        [TestMethod] // BUG-003
        public void History_NotSharedBetweenInstances()
        {
            var calc1 = new Calculator();
            var calc2 = new Calculator();
            calc1.Add(1, 1);
            calc2.GetHistory().Should().BeEmpty();
        }

        [TestMethod] // BUG-004
        public void Sqrt_OfZero_ReturnsZero()
        {
            new Calculator().Sqrt(0).Should().Be(0);
        }

        [TestMethod] // BUG-005
        public void Multiply_TwoNegatives_ReturnsPositive()
        {
            new Calculator().Multiply(-3, -4).Should().Be(12);
        }
    }
}