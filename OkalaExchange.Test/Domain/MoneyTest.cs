using FluentAssertions;
using OkalaExchange.Domain.Exchage.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkalaExchange.Test.Domain
{
    public class MoneyTest
    {
        [Fact]
        public void Constructor_ShouldInitializePropertiesAndUppercaseCurrency()
        {
            var money = new Money(123.456m, "usd");

            money.Amount.Should().Be(123.46m); // rounded
            money.Currency.Should().Be("USD");
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenCurrencyIsNullOrWhitespace()
        {
            Action act = () => new Money(100m, null!);
            act.Should().Throw<ArgumentException>().WithMessage("*Currency code is required*");

            act = () => new Money(100m, "  ");
            act.Should().Throw<ArgumentException>().WithMessage("*Currency code is required*");
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenAmountAndCurrencyAreEqual()
        {
            var m1 = new Money(100.00m, "usd");
            var m2 = new Money(100.000m, "USD");

            m1.Equals(m2).Should().BeTrue();
            m1.Equals((object)m2).Should().BeTrue();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenAmountOrCurrencyDiffer()
        {
            var m1 = new Money(100.00m, "USD");
            var m2 = new Money(99.99m, "USD");
            var m3 = new Money(100.00m, "EUR");

            m1.Equals(m2).Should().BeFalse();
            m1.Equals(m3).Should().BeFalse();
        }

        [Fact]
        public void GetHashCode_ShouldBeEqual_ForEqualMoneyObjects()
        {
            var m1 = new Money(100.00m, "usd");
            var m2 = new Money(100.000m, "USD");

            m1.GetHashCode().Should().Be(m2.GetHashCode());
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            var money = new Money(123.456m, "eur");

            money.ToString().Should().Be("123.46 EUR");
        }
    }
}
