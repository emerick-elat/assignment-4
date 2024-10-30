using FluentValidation;

namespace Bank.UseCases.Account.QueryGetAccount
{
    internal class GetAccountQueryValidators : AbstractValidator<GetAccountQuery>
    {
        public GetAccountQueryValidators()
        {
            RuleFor(a => a.AccountNumber)
                .NotEmpty().WithMessage("Account cannot be empty")
                .NotNull().WithMessage("Please provide an account number")
                .Length(16);

            RuleFor(a => a.Currency)
                .Length(3);
        }
    }
}
