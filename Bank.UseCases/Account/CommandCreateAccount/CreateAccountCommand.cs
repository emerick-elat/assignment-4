using MediatR;

namespace Bank.UseCases.Account.CommandCreateAccount
{
    public class CreateAccountCommand : IRequest<AccountDto>
    {
        public int? CustomerId {  get; set; }
    }
}
