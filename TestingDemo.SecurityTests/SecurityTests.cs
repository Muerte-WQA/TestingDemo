using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestingDemo.Core;

namespace TestingDemo.SecurityTests
{
    [TestClass]
    public class SecurityTests
    {
        private static Dictionary<string, string> GetUsers(UserService service)
        {
            var field = typeof(UserService)
                .GetField("_users", BindingFlags.NonPublic | BindingFlags.Instance);
            return (Dictionary<string, string>)field.GetValue(service);
        }

        [TestMethod]
        public void Password_NotStoredInPlainText()
        {
            var service = new UserService();
            service.Register("alice", "MySecret123");
            GetUsers(service).Values.Should().NotContain("MySecret123");
        }

        [TestMethod]
        public void WrongPassword_Throws()
        {
            var service = new UserService();
            service.Register("alice", "CorrectPass1");
            Action act = () => service.Login("alice", "WrongPass");
            act.Should().Throw<ArgumentException>().WithMessage("*Неверный пароль*");
        }

        [TestMethod]
        public void UnknownUser_Throws()
        {
            var service = new UserService();
            Action act = () => service.Login("ghost", "any");
            act.Should().Throw<ArgumentException>().WithMessage("*не найден*");
        }

        [TestMethod]
        public void WeakPassword_Rejected()
        {
            var service = new UserService();
            Action act = () => service.Register("bob", "123");
            act.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void SessionTokens_AreUnique()
        {
            var service = new UserService();
            service.Register("alice", "StrongPass1");
            var tokens = Enumerable.Range(0, 5)
                .Select(_ => service.Login("alice", "StrongPass1"))
                .Distinct()
                .ToList();
            tokens.Should().HaveCount(5);
        }
    }
}