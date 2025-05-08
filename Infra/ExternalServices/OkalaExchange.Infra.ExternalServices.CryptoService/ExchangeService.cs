using Microsoft.Extensions.Configuration;
using OkalaExchange.Contract.Exchange.ExternalServices;
using OkalaExchange.Contracts.Exchange.ExternalServices;
using System.Net.Http.Json;
using System.Text.Json;


namespace OkalaExchange.Infra.ExternalServices.CryptoService
{
    public class ExchangeService(HttpClient httpClient, IConfiguration config) : IExchangeService
    {
        private readonly string _coinMarketCapApiKey = config["CoinMarketCap:ApiKey"]!;
        private readonly string _coinMarketCapBaseUrl = config["CoinMarketCap:BaseUrl"]!;

        private readonly string _exchangeRatesApiKey = config["ExchangeRatesApi:ApiKey"]!;
        private readonly string _exchangeRatesBaseUrl = config["ExchangeRatesApi:BaseUrl"]!;

        private readonly HttpClient _httpClient = httpClient;




        public async Task<decimal> GetCryptoPriceAsync(string cryptoCode, string targetCurrency)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", _coinMarketCapApiKey);

            var url = $"{_coinMarketCapBaseUrl}/v1/cryptocurrency/quotes/latest?symbol={cryptoCode}&convert={targetCurrency}";
            var responseStream = await _httpClient.GetStreamAsync(url);

            using var jsonDoc = await JsonDocument.ParseAsync(responseStream);
            var root = jsonDoc.RootElement;

            decimal price = root
                .GetProperty("data")
                .GetProperty(cryptoCode.ToUpper())
                .GetProperty("quote")
                .GetProperty(targetCurrency.ToUpper())
                .GetProperty("price")
                .GetDecimal();

            return price;
        }
        public async Task<Dictionary<string, decimal>> GetExchangeRatesAsync(string baseCurrency, IEnumerable<string> currencies)
        {

            var symbols = string.Join(",", currencies);
            var url = $"{_exchangeRatesBaseUrl}/latest?base={baseCurrency}&symbols={symbols}&apikey={_exchangeRatesApiKey}";

            var response = await _httpClient.GetFromJsonAsync<dynamic>(url);
            var result = JsonSerializer.Deserialize<ExchangeRateResponse>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            var rates = new Dictionary<string, decimal>();
            foreach (var currency in currencies)
            {
                rates[currency] = (decimal)result.Rates[currency];
            }

            return rates;
        }
    }



}
