using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CurrencyExchangeRateLib
{
    public class HttpCurrencyExchangeRate : ICurrencyExchangeRate
    {
        private readonly HttpClient _httpClient;

        public HttpCurrencyExchangeRate(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetRate(string fromCurrency, string toCurrency)
        {
            var json = await _httpClient.GetStringAsync($"https://api.exchangeratesapi.io/latest?base={fromCurrency}&symbols={toCurrency}");
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);
            var rates = jsonElement.GetProperty("rates");
            return rates.GetProperty(toCurrency).GetDecimal();
        }

        public async Task<IEnumerable<string>> GetCurrencies()
        {
            var json = await _httpClient.GetStringAsync("https://api.exchangeratesapi.io/latest");
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);
            var rates = jsonElement.GetProperty("rates");
            return rates.EnumerateObject().Select(jsonProperty => jsonProperty.Name).OrderBy(rate => rate);
        }
    }
}
