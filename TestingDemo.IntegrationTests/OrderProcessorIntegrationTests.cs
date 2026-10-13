using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestingDemo.Core;

namespace TestingDemo.IntegrationTests
{
    [TestClass]
    public class OrderProcessorIntegrationTests
    {
        [TestMethod]
        public void CalculateTotal_WithRealTaxAndDiscount_IntegratesCorrectly()
        {
            var discountProvider = new Mock<IDiscountProvider>();
            discountProvider.Setup(x => x.GetDiscount("user1")).Returns(0.10m);

            var taxCalculator = new Mock<ITaxCalculator>();
            taxCalculator
                .Setup(x => x.CalculateTax(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns<decimal, decimal>((amount, rate) => amount * rate);

            var processor = new OrderProcessor(discountProvider.Object, taxCalculator.Object);

            var order = new Order { TaxRate = 0.20m };
            order.AddItem(new OrderItem("Book", 100m, 2));

            var total = processor.CalculateTotal(order, "user1");

            total.Should().Be(216m);
            discountProvider.Verify(x => x.GetDiscount("user1"), Times.Once);
            taxCalculator.Verify(x => x.CalculateTax(180m, 0.20m), Times.Once);
        }

        [TestMethod]
        public void CalculateTotal_EmptyOrder_ThrowsBeforeCallingDependencies()
        {
            var discountProvider = new Mock<IDiscountProvider>();
            var taxCalculator = new Mock<ITaxCalculator>();
            var processor = new OrderProcessor(discountProvider.Object, taxCalculator.Object);

            Action act = () => processor.CalculateTotal(new Order(), "user1");

            act.Should().Throw<InvalidOperationException>();
            discountProvider.Verify(x => x.GetDiscount(It.IsAny<string>()), Times.Never);
            taxCalculator.Verify(
                x => x.CalculateTax(It.IsAny<decimal>(), It.IsAny<decimal>()), Times.Never);
        }
    }
}