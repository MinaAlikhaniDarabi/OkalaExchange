using FluentAssertions;
using OkalaExchange.Domain.Exchage.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkalaExchange.Test.Domain
{
    public class CryptoQuoteTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties_WhenValidInput()
        {
            var prices = new List<Money>
        {
            new Money(1000m, "USD"),
            new Money(900m, "EUR")
        };

            var quote = new CryptoQuote("btc", prices);

            quote.CryptoCode.Should().Be("BTC");
            quote.Prices.Should().BeEquivalentTo(prices);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenCryptoCodeIsNullOrEmpty()
        {
            var prices = new List<Money> { new Money(1000m, "USD") };

            Action act = () => new CryptoQuote(null!, prices);
            act.Should().Throw<ArgumentException>().WithMessage("*Crypto code cannot be null or empty*");

            act = () => new CryptoQuote("   ", prices);
            act.Should().Throw<ArgumentException>().WithMessage("*Crypto code cannot be null or empty*");
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenPricesIsNull()
        {
            Action act = () => new CryptoQuote("BTC", null!);
            act.Should().Throw<ArgumentNullException>().WithMessage("*prices*");
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenPricesIsEmpty()
        {
            Action act = () => new CryptoQuote("BTC", new List<Money>());
            act.Should().Throw<ArgumentException>().WithMessage("*Prices cannot be empty*");
        }

        [Fact]
        public void GetPriceForCurrency_ShouldReturnCorrectPrice_WhenCurrencyExists()
        {
            var prices = new List<Money>
        {
            new Money(1000m, "USD"),
            new Money(900m, "EUR")
        };
            var quote = new CryptoQuote("BTC", prices);

            var result = quote.GetPriceForCurrency("eur");

            result.Should().NotBeNull();
            result!.Amount.Should().Be(900m);
        }

        [Fact]
        public void GetPriceForCurrency_ShouldReturnNull_WhenCurrencyDoesNotExist()
        {
            var prices = new List<Money>
        {
            new Money(1000m, "USD")
        };
            var quote = new CryptoQuote("BTC", prices);

            var result = quote.GetPriceForCurrency("JPY");

            result.Should().BeNull();
        }
    }
}
