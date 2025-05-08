using FluentAssertions;
using Moq;
using OkalaExchange.ApplicationService.Exchange.Queries;
using OkalaExchange.Contracts.Exchange.ExternalServices;
using OkalaExchange.Domain.Exchage.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkalaExchange.Test.ApplicationService
{

    public class GetLatestQuoteQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnCorrectCryptoQuote()
        {
            // Arrange
            var cryptoCode = "BTC";
            var baseCurrency = "USD";
            var targetCurrencies = new[] { "EUR", "BRL", "GBP", "AUD" };
            var basePrice = 50000m;

            var mockExchangeService = new Mock<IExchangeService>();

            mockExchangeService
                .Setup(s => s.GetCryptoPriceAsync(cryptoCode, baseCurrency))
                .ReturnsAsync(basePrice);

            var exchangeRates = new Dictionary<string, decimal>
        {
            { "EUR", 0.9m },
            { "BRL", 5.0m },
            { "GBP", 0.8m },
            { "AUD", 1.5m }
        };

            mockExchangeService
                .Setup(s => s.GetExchangeRatesAsync(baseCurrency, targetCurrencies))
                .ReturnsAsync(exchangeRates);

            var handler = new GetLatestQuoteQueryHandler(mockExchangeService.Object);

            var query = new GetLatestQuoteQuery
            {
                CryptoCode = cryptoCode,
                TargetCurrencies = targetCurrencies
            };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.CryptoCode.Should().Be("BTC");
            result.Prices.Should().ContainEquivalentOf(new Money(basePrice, "USD"));
            result.Prices.Should().ContainEquivalentOf(new Money(basePrice * 0.9m, "EUR"));
            result.Prices.Should().ContainEquivalentOf(new Money(basePrice * 5.0m, "BRL"));
            result.Prices.Should().ContainEquivalentOf(new Money(basePrice * 0.8m, "GBP"));
            result.Prices.Should().ContainEquivalentOf(new Money(basePrice * 1.5m, "AUD"));
        }
    }
}
