using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Account.QueryGetAccount
{
    internal class GetAccountQueryValidators : AbstractValidator<GetAccountQuery>
    {
        public GetAccountQueryValidators()
        {
            RuleFor(a => a.AccountNumber)
                .NotEmpty().WithMessage("Account cannot be empty")
                .NotNull().WithMessage("Please provide an account number");
        }
    }
}
