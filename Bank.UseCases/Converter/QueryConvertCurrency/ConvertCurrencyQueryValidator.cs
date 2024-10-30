using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Converter.QueryConvertCurrency
{
    internal class ConvertCurrencyQueryValidator : AbstractValidator<ConvertCurrencyQuery>
    {
        public ConvertCurrencyQueryValidator() {

            RuleFor(c =>  c.Currency)
                .NotNull().WithMessage("Currency cannot be null")
                .NotEmpty().WithMessage("Currency cannot be empty")
                .MaximumLength(3).WithMessage("Invalid Currency format, Ex: EUR, USD...");

            RuleFor(c =>  c.TargetCurrency)
                .NotNull().WithMessage("Target Currency cannot be null")
                .NotEmpty().WithMessage("Target Currency cannot be empty")
                .MaximumLength(3).WithMessage("Invalid Currency format, Ex: EUR, USD...");

            RuleFor(c =>  c.Amount)
                .NotNull().WithMessage("Amount to convert cannot be null")
                .NotEmpty().WithMessage("Amount to convert cannot be empty")
                .GreaterThan(0).WithMessage("Amount to convert should be greater than 0");
        }
    }
}
