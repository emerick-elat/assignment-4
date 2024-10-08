using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Account.CommandCreateAccount
{
    internal class CreateAccountCommandValidation : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidation()
        {
            RuleFor(a => a.CustomerId)
                .NotEmpty().WithMessage("Customer's name cannot be empty")
                .NotNull().WithMessage("PLease provide customer's ID");
        }
    }
}
