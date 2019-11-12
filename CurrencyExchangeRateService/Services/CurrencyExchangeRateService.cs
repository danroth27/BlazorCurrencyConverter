using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyExchangeRateLib;
using Google.Protobuf.Collections;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace CurrencyExchangeRateService
{
    public class CurrencyExchangeRateService : CurrencyExchangeRate.CurrencyExchangeRateBase
    {
        private readonly ICurrencyExchangeRate _currencyExchangeRate;

        public CurrencyExchangeRateService(ICurrencyExchangeRate currencyExchangeRate)
        {
            _currencyExchangeRate = currencyExchangeRate;
        }

        public override async Task<CurrenciesReply> GetCurrencies(CurrenciesRequest request, ServerCallContext context)
        {
            var currencies = await _currencyExchangeRate.GetCurrencies();
            var reply = new CurrenciesReply();
            reply.Currencies.AddRange(currencies);
            return reply;
        }

        public override async Task<ExchangeRateReply> GetExchangeRate(ExchangeRateRequest request, ServerCallContext context)
        {
            var reply = new ExchangeRateReply();
            var rate = await _currencyExchangeRate.GetRate(request.CurrencyTypeFrom, request.CurrencyTypeTo);
            reply.ExchangeRate = (double)rate;
            return reply;
        }
    }
}
