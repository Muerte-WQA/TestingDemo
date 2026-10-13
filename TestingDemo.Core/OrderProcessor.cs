using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingDemo.Core
{
    public class OrderItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public OrderItem(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }

    public class Order
    {
        public List<OrderItem> Items { get; } = new List<OrderItem>();
        public decimal Discount { get; set; }
        public decimal TaxRate { get; set; } = 0.20m;

        public void AddItem(OrderItem item) => Items.Add(item);
    }

    public interface IDiscountProvider
    {
        decimal GetDiscount(string userId);
    }

    public interface ITaxCalculator
    {
        decimal CalculateTax(decimal amount, decimal rate);
    }

    public class OrderProcessor
    {
        private readonly IDiscountProvider _discountProvider;
        private readonly ITaxCalculator _taxCalculator;

        public OrderProcessor(IDiscountProvider discountProvider, ITaxCalculator taxCalculator)
        {
            _discountProvider = discountProvider;
            _taxCalculator = taxCalculator;
        }

        public decimal CalculateTotal(Order order, string userId)
        {
            if (order.Items.Count == 0)
                throw new InvalidOperationException("Пустой заказ");

            var subtotal = order.Items.Sum(i => i.Price * i.Quantity);
            var discount = _discountProvider.GetDiscount(userId);
            var afterDiscount = subtotal * (1 - discount);
            var tax = _taxCalculator.CalculateTax(afterDiscount, order.TaxRate);
            return afterDiscount + tax;
        }
    }
}