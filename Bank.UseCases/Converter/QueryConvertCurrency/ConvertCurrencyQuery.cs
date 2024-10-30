using Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Converter.QueryConvertCurrency
{
    internal class ConvertCurrencyQuery : IRequest<ExchangeRate>
    {
        public decimal Amount { get; set; }
        public string? Currency {  get; set; }
        public string? TargetCurrency { get; set; }
    }
}
