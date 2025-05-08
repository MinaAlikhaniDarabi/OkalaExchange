using OkalaExchange.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkalaExchange.Domain.Exchage.ValueObjects
{
    public class CryptoQuote:ValueObject
    {
        public string CryptoCode { get; }
        public IReadOnlyCollection<Money> Prices { get; }

        public CryptoQuote(string cryptoCode, IEnumerable<Money> prices)
        {
            if (string.IsNullOrWhiteSpace(cryptoCode))
                throw new ArgumentException("Crypto code cannot be null or empty.", nameof(cryptoCode));

            CryptoCode = cryptoCode.ToUpper();
            Prices = prices?.ToList().AsReadOnly() ?? throw new ArgumentNullException(nameof(prices));

            if (!Prices.Any())
                throw new ArgumentException("Prices cannot be empty.", nameof(prices));
        }

        public Money? GetPriceForCurrency(string currencyCode)
        {
            return Prices.FirstOrDefault(p => p.Currency == currencyCode.ToUpper());
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CryptoCode;

            foreach (var price in Prices.OrderBy(p => p.Currency)) 
            {
                yield return price;
            }
        }
    }
}
