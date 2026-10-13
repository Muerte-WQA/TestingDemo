using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestingDemo.Core;

namespace TestingDemo.UnitTests
{
    [TestClass]
    public class CalculatorUnitTests
    {
        private readonly Calculator _calc = new Calculator();

        [TestMethod]
        public void Add_TwoPositiveNumbers_ReturnsSum()
        {
            _calc.Add(2, 3).Should().Be(5);
        }

        [TestMethod]
        public void Add_ZeroAndFive_ReturnsFive()
        {
            _calc.Add(0, 5).Should().Be(5);
        }

        [TestMethod]
        public void Add_NegativeAndPositive_ReturnsZero()
        {
            _calc.Add(-1, 1).Should().Be(0);
        }

        [TestMethod]
        public void Add_LargeNumbers_ReturnsCorrectSum()
        {
            _calc.Add(100, 200).Should().Be(300);
        }

        [TestMethod]
        public void Divide_ByZero_ThrowsDivisionByZeroException()
        {
            Action act = () => _calc.Divide(1, 0);
            act.Should().Throw<DivisionByZeroException>();
        }

        [TestMethod]
        public void Sqrt_NegativeInput_ThrowsCalculatorException()
        {
            Action act = () => _calc.Sqrt(-4);
            act.Should().Throw<CalculatorException>()
               .WithMessage("*отрицательного*");
        }

        [TestMethod]
        public void History_AfterTwoOperations_HasTwoEntries()
        {
            _calc.Add(1, 2);
            _calc.Multiply(3, 4);
            _calc.GetHistory().Should().HaveCount(2);
        }

        [TestMethod]
        public void ClearHistory_RemovesAllEntries()
        {
            _calc.Add(1, 2);
            _calc.ClearHistory();
            _calc.GetHistory().Should().BeEmpty();
        }
    }
}