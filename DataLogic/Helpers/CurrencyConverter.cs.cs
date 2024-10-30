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
                new ExchangeRate { Currency = "USD", Value = 1.0774m },
                new ExchangeRate { Currency = "JPY", Value = 165.66m },
                new ExchangeRate { Currency = "BGN", Value = 1.9558m },
                new ExchangeRate { Currency = "CZK", Value = 25.372m },
                new ExchangeRate { Currency = "DKK", Value = 7.4588m },
                new ExchangeRate { Currency = "GBP", Value = 0.83020m },
                new ExchangeRate { Currency = "HUF", Value = 405.23m },
                new ExchangeRate { Currency = "PLN", Value = 4.3480m },
                new ExchangeRate { Currency = "RON", Value = 4.9749m },
                new ExchangeRate { Currency = "SEK", Value = 11.5170m },
                new ExchangeRate { Currency = "CHF", Value = 0.9369m },
                new ExchangeRate { Currency = "ISK", Value = 148.70m },
                new ExchangeRate { Currency = "NOK", Value = 11.8415m },
                new ExchangeRate { Currency = "TRY", Value = 36.9491m },
                new ExchangeRate { Currency = "AUD", Value = 1.6423m },
                new ExchangeRate { Currency = "BRL", Value = 6.1613m },
                new ExchangeRate { Currency = "CAD", Value = 1.4981m },
                new ExchangeRate { Currency = "CNY", Value = 7.6895m },
                new ExchangeRate { Currency = "HKD", Value = 8.3716m },
                new ExchangeRate { Currency = "IDR", Value = 16993.40m },
                new ExchangeRate { Currency = "ILS", Value = 4.0298m },
                new ExchangeRate { Currency = "INR", Value = 90.5700m },
                new ExchangeRate { Currency = "KRW", Value = 1496.40m },
                new ExchangeRate { Currency = "MXN", Value = 21.5719m },
                new ExchangeRate { Currency = "MYR", Value = 4.7172m },
                new ExchangeRate { Currency = "NZD", Value = 1.8076m },
                new ExchangeRate { Currency = "PHP", Value = 62.857m },
                new ExchangeRate { Currency = "SGD", Value = 1.4287m },
                new ExchangeRate { Currency = "THB", Value = 36.400m },
                new ExchangeRate { Currency = "ZAR", Value = 19.1063m }
            };
        }

        public Task<ExchangeRate> ConvertCurrency(decimal Amount, string Currency, string TargetCurrency)
        {
            var exchangeRate = ExchangeRates.FirstOrDefault(er => er.Currency == TargetCurrency);
            decimal convertedAmount = 0m;
            if (exchangeRate is not null)
            {
                convertedAmount = Amount * exchangeRate.Value;
            }

            return Task.FromResult(new ExchangeRate
            {
                Currency = TargetCurrency,
                Value = convertedAmount
            });
        }
    }
}
