using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestingDemo.Core;

namespace TestingDemo.FunctionalTests
{
    [TestClass]
    public class FunctionalTests
    {
        private readonly Calculator _calc = new Calculator();

        [TestMethod] // FR-1
        public void FR1_SupportsFiveOperations()
        {
            _calc.Add(1, 1).Should().Be(2);
            _calc.Subtract(2, 1).Should().Be(1);
            _calc.Multiply(2, 2).Should().Be(4);
            _calc.Divide(4, 2).Should().Be(2);
            _calc.Power(2, 3).Should().Be(8);
        }

        [TestMethod] // FR-2
        public void FR2_HistoryAvailable()
        {
            _calc.Add(1, 2);
            _calc.GetHistory().Should().NotBeEmpty();
        }

        [TestMethod] // FR-3
        public void FR3_PasswordMinLength()
        {
            var service = new UserService();
            Action act = () => service.Register("bob", "short");
            act.Should().Throw<ArgumentException>().WithMessage("*8 символов*");
        }

        [TestMethod] // FR-4
        public void FR4_UniqueUsername()
        {
            var service = new UserService();
            service.Register("bob", "Password123").Should().BeTrue();
            service.Register("bob", "Password456").Should().BeFalse();
        }

        [TestMethod] // FR-5
        public void FR5_PasswordHashed()
        {
            var service = new UserService();
            service.Register("bob", "Password123");

            var field = typeof(UserService)
                .GetField("_users", BindingFlags.NonPublic | BindingFlags.Instance);
            var users = (Dictionary<string, string>)field.GetValue(service);
            users["bob"].Should().NotBe("Password123");
            users["bob"].Should().HaveLength(64);
        }
    }
}