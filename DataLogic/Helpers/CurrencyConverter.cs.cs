using Azure.Core;
using Entities;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Helpers
{
    public class CurrencyConverter(IDistributedCache _cache) : ICurrencyConverter
    {   
        public async Task<ExchangeRate> ConvertCurrency(decimal Amount, string Currency, string TargetCurrency)
        {
            ICollection<ExchangeRate> exchangeRates;
            var cachedResults = await _cache.GetAsync("ExchangeRates");
            if (cachedResults is null)
            {
                exchangeRates = await GetExchangeRates();
            }
            else
            {
                exchangeRates = ConvertBytesToList(cachedResults);
            }
            
            var exchangeRate = exchangeRates.FirstOrDefault(er => er.Currency == TargetCurrency);
            decimal convertedAmount = 0m;
            if (exchangeRate is not null)
            {
                convertedAmount = Amount * exchangeRate.Value;
            }

            return new ExchangeRate
            {
                Currency = TargetCurrency,
                Value = convertedAmount
            };
        }

        private async Task<ICollection<ExchangeRate>> GetExchangeRates()
        {
            string url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
            ICollection<ExchangeRate> exchangeRates = new List<ExchangeRate>
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
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                XDocument doc = XDocument.Parse(response);

                foreach (var cube in doc.Descendants("{http://www.ecb.int/vocabulary/2002-08-01/eurofxref}Cube"))
                {
                    var currency = cube.Attribute("currency")?.Value;
                    var rateStr = cube.Attribute("rate")?.Value;
                    decimal rate;
                    decimal.TryParse(rateStr, out rate);

                    if (currency != null && rateStr != null)
                    {
                        exchangeRates.Add(new ExchangeRate { Currency = currency, Value = rate });
                    }
                }
            }
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1));
            byte[] encodedexchangeRates = ConvertListToByte(exchangeRates);
            await _cache.SetAsync("ExchangeRates", encodedexchangeRates, options);
            Task.Delay(TimeSpan.FromSeconds(20)).Wait();
            return exchangeRates;
        }

        private byte[] ConvertListToByte(ICollection<ExchangeRate> exchangeRates)
        {   
            var exchangeRateString = JsonSerializer.Serialize(exchangeRates, new JsonSerializerOptions
            {
                WriteIndented = true,
            });
            byte[] encodedexchangeRates = Encoding.UTF8.GetBytes(exchangeRateString);
            return encodedexchangeRates;
        }

        private ICollection<ExchangeRate> ConvertBytesToList(byte[] bytes)
        {
            var jsonString = Encoding.UTF8.GetString(bytes);
            var exchangeRates = JsonSerializer.Deserialize<ExchangeRate[]>(jsonString);
            return exchangeRates.ToList();
        }
    }
}
