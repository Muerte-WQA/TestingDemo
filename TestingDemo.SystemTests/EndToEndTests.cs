using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestingDemo.Core;

namespace TestingDemo.SystemTests
{
    [TestClass]
    public class EndToEndTests
    {
        [TestMethod]
        public void FullUserJourney_RegisterLoginAndCalculate()
        {
            var service = new UserService();
            var calc = new Calculator();

            service.Register("alice", "StrongPass123").Should().BeTrue();

            var token = service.Login("alice", "StrongPass123");
            token.Should().NotBeNullOrEmpty().And.HaveLength(64);

            calc.Add(10, 20).Should().Be(30);
            calc.Multiply(5, 6).Should().Be(30);
            calc.Divide(100, 4).Should().Be(25);
        }

        [TestMethod]
        public void FullOrderProcessing_Pipeline_WorksEndToEnd()
        {
            var discountProvider = new Mock<IDiscountProvider>();
            discountProvider.Setup(x => x.GetDiscount(It.IsAny<string>())).Returns(0.15m);
            var taxCalculator = new Mock<ITaxCalculator>();
            taxCalculator
                .Setup(x => x.CalculateTax(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns<decimal, decimal>((a, r) => a * r);

            var processor = new OrderProcessor(discountProvider.Object, taxCalculator.Object);
            var order = new Order();
            order.AddItem(new OrderItem("Pen", 10m, 5));
            order.AddItem(new OrderItem("Notebook", 50m, 2));

            processor.CalculateTotal(order, "alice").Should().Be(153m);
        }
    }
}