using Entities;
using Infrastructure.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Converter.QueryConvertCurrency
{
    internal class ConvertCurrencyQueryHandler (ICurrencyConverter currencyConverter)
        : IRequestHandler<ConvertCurrencyQuery, ExchangeRate>
    {   

        public async Task<ExchangeRate> Handle(ConvertCurrencyQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ConvertCurrencyQueryValidator validator = new ConvertCurrencyQueryValidator();
            var result = validator.Validate(request);
            if (!result.IsValid)
            {
                throw new ArgumentException(nameof(request));
            }
            return await currencyConverter.ConvertCurrency(request.Amount, request.Currency!, request.TargetCurrency!);
        }
    }
}
