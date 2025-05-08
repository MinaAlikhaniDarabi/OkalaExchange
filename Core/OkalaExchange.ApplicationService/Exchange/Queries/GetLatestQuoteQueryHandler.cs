using MediatR;
using OkalaExchange.Contracts.Exchange.ExternalServices;
using OkalaExchange.Domain.Exchage.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkalaExchange.ApplicationService.Exchange.Queries
{
    public class GetLatestQuoteQueryHandler : IRequestHandler<GetLatestQuoteQuery, CryptoQuote>
    {
        private readonly IExchangeService _exchangeService;

        public GetLatestQuoteQueryHandler(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }
        public async Task<CryptoQuote> Handle(GetLatestQuoteQuery request, CancellationToken cancellationToken)
        {
            var cryptoCode = request.CryptoCode;
            var baseCurrency = "USD";
            var targetCurrencies = request.TargetCurrencies;

            decimal basePrice = await _exchangeService.GetCryptoPriceAsync(cryptoCode, baseCurrency);

            var exchangeRates = await _exchangeService.GetExchangeRatesAsync(baseCurrency, targetCurrencies);

            var prices = new List<Money>
    {
        new Money(basePrice, baseCurrency)
    };

            foreach (var currency in targetCurrencies)
            {
                decimal converted = basePrice * exchangeRates[currency];
                prices.Add(new Money(converted, currency));
            }

            return new CryptoQuote(cryptoCode.ToUpper(), prices);
        }
    }
}
