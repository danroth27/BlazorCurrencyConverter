using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyExchangeRateLib
{
    public interface ICurrencyExchangeRate
    {
        Task<decimal> GetRate(string fromCurrency, string toCurrency);

        Task<IEnumerable<string>> GetCurrencies();
    }
}
