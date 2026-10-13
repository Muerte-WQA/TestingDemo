using System;
using System.Collections.Generic;

namespace TestingDemo.Core
{
    public class CalculatorException : Exception
    {
        public CalculatorException(string message) : base(message) { }
    }

    public class DivisionByZeroException : CalculatorException
    {
        public DivisionByZeroException() : base("Деление на ноль невозможно") { }
    }

    public class Calculator
    {
        private readonly List<string> _history = new List<string>();

        public double Add(double a, double b)
        {
            var result = a + b;
            Log(string.Format("{0} + {1} = {2}", a, b, result));
            return result;
        }

        public double Subtract(double a, double b)
        {
            var result = a - b;
            Log(string.Format("{0} - {1} = {2}", a, b, result));
            return result;
        }

        public double Multiply(double a, double b)
        {
            var result = a * b;
            Log(string.Format("{0} * {1} = {2}", a, b, result));
            return result;
        }

        public double Divide(double a, double b)
        {
            if (b == 0) throw new DivisionByZeroException();
            var result = a / b;
            Log(string.Format("{0} / {1} = {2}", a, b, result));
            return result;
        }

        public double Power(double baseValue, double exponent)
        {
            var result = Math.Pow(baseValue, exponent);
            Log(string.Format("{0} ^ {1} = {2}", baseValue, exponent, result));
            return result;
        }

        public double Sqrt(double x)
        {
            if (x < 0) throw new CalculatorException("Корень из отрицательного числа");
            return Math.Sqrt(x);
        }

        public IReadOnlyList<string> GetHistory() => _history.AsReadOnly();

        public void ClearHistory() => _history.Clear();

        private void Log(string message) => _history.Add(message);
    }
}