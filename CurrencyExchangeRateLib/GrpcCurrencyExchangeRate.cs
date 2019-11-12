using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CurrencyExchangeRateService;

namespace CurrencyExchangeRateLib
{
    public class GrpcCurrencyExchangeRate : ICurrencyExchangeRate
    {
        private readonly CurrencyExchangeRate.CurrencyExchangeRateClient _client;

        public GrpcCurrencyExchangeRate(CurrencyExchangeRate.CurrencyExchangeRateClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<string>> GetCurrencies()
        {
            var currenciesReply = await _client.GetCurrenciesAsync(new CurrenciesRequest());
            return currenciesReply.Currencies;
        }

        public async Task<decimal> GetRate(string fromCurrency, string toCurrency)
        {
            var request = new ExchangeRateRequest { CurrencyTypeFrom = fromCurrency, CurrencyTypeTo = toCurrency };
            var reply = await _client.GetExchangeRateAsync(request);
            return (decimal)reply.ExchangeRate;
        }
    }
}
