using Azure.Core;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public class CurrencyConverter : ICurrencyConverter
    {
        private readonly ICollection<ExchangeRate> ExchangeRates;
        public CurrencyConverter()
        {
            ExchangeRates = new List<ExchangeRate>
            {
                new ExchangeRate { Currency = "USD", Rate = 1.0774m },
                new ExchangeRate { Currency = "JPY", Rate = 165.66m },
                new ExchangeRate { Currency = "BGN", Rate = 1.9558m },
                new ExchangeRate { Currency = "CZK", Rate = 25.372m },
                new ExchangeRate { Currency = "DKK", Rate = 7.4588m },
                new ExchangeRate { Currency = "GBP", Rate = 0.83020m },
                new ExchangeRate { Currency = "HUF", Rate = 405.23m },
                new ExchangeRate { Currency = "PLN", Rate = 4.3480m },
                new ExchangeRate { Currency = "RON", Rate = 4.9749m },
                new ExchangeRate { Currency = "SEK", Rate = 11.5170m },
                new ExchangeRate { Currency = "CHF", Rate = 0.9369m },
                new ExchangeRate { Currency = "ISK", Rate = 148.70m },
                new ExchangeRate { Currency = "NOK", Rate = 11.8415m },
                new ExchangeRate { Currency = "TRY", Rate = 36.9491m },
                new ExchangeRate { Currency = "AUD", Rate = 1.6423m },
                new ExchangeRate { Currency = "BRL", Rate = 6.1613m },
                new ExchangeRate { Currency = "CAD", Rate = 1.4981m },
                new ExchangeRate { Currency = "CNY", Rate = 7.6895m },
                new ExchangeRate { Currency = "HKD", Rate = 8.3716m },
                new ExchangeRate { Currency = "IDR", Rate = 16993.40m },
                new ExchangeRate { Currency = "ILS", Rate = 4.0298m },
                new ExchangeRate { Currency = "INR", Rate = 90.5700m },
                new ExchangeRate { Currency = "KRW", Rate = 1496.40m },
                new ExchangeRate { Currency = "MXN", Rate = 21.5719m },
                new ExchangeRate { Currency = "MYR", Rate = 4.7172m },
                new ExchangeRate { Currency = "NZD", Rate = 1.8076m },
                new ExchangeRate { Currency = "PHP", Rate = 62.857m },
                new ExchangeRate { Currency = "SGD", Rate = 1.4287m },
                new ExchangeRate { Currency = "THB", Rate = 36.400m },
                new ExchangeRate { Currency = "ZAR", Rate = 19.1063m }
            };
        }

        public Task<decimal> ConvertCurrency(decimal Amount, string Currency, string TargetCurrency)
        {
            var exchangeRate = ExchangeRates.FirstOrDefault(er => er.Currency == TargetCurrency);
            decimal convertedAmount = 0m;
            if (exchangeRate is not null)
            {
                convertedAmount = Amount * exchangeRate.Rate;
            }

            return Task.FromResult(convertedAmount);
        }
    }
}
