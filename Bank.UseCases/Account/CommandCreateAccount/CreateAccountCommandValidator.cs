using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Account.CommandCreateAccount
{
    internal class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(a => a.CustomerId)
                .GreaterThanOrEqualTo(0).WithMessage("Customer ID should be a positive number");
        }
    }
}
