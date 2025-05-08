using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkalaExchange.Contracts.Exchange.ExternalServices
{
    public interface IExchangeService
    {
        Task<decimal> GetCryptoPriceAsync(string cryptoCode, string targetCurrency);
        Task<Dictionary<string, decimal>> GetExchangeRatesAsync(string baseCurrency, IEnumerable<string> currencies);
    }
}
