using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public interface ICurrencyConverter
    {
        Task<decimal> ConvertCurrency(decimal Amount, string Currency, string TargetCurrency);
    }
}
