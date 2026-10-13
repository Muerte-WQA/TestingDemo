using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestingDemo.Core;

namespace TestingDemo.AcceptanceTests
{
    [TestClass]
    public class AcceptanceTests
    {
        [TestMethod] // AC-1
        public void AC1_BasicCalculation()
        {
            var calc = new Calculator();
            calc.Add(2, 2).Should().Be(4);
        }

        [TestMethod] // AC-2
        public void AC2_RegistrationAndLogin()
        {
            var service = new UserService();
            service.Register("ivan", "SecurePass123").Should().BeTrue();
            var token = service.Login("ivan", "SecurePass123");
            token.Should().NotBeNullOrEmpty().And.HaveLength(64);
        }

        [TestMethod] // AC-3
        public void AC3_ClearErrorMessageOnDivision()
        {
            var calc = new Calculator();
            Action act = () => calc.Divide(1, 0);
            act.Should().Throw<CalculatorException>().WithMessage("*ноль*");
        }

        [TestMethod] // AC-4
        public void AC4_OperationHistory()
        {
            var calc = new Calculator();
            calc.Add(1, 1);
            calc.Subtract(5, 3);
            calc.GetHistory().Should().HaveCount(2);
        }

        [TestMethod] // AC-5
        public void AC5_PasswordPolicyEnforced()
        {
            var service = new UserService();
            Action act = () => service.Register("petr", "123");
            act.Should().Throw<ArgumentException>().WithMessage("*8 символов*");
        }
    }
}