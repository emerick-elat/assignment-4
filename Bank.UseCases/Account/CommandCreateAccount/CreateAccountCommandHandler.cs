using AutoMapper;
using Infrastructure.BankAccountRepository.Contract;
using MediatR;
using Entities;

namespace Bank.UseCases.Account.CommandCreateAccount
{
    internal class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDto>
    {
        private readonly IBankAccountRepository _repo;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateAccountCommandHandler(IBankAccountRepository repo,
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _repo = repo;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<AccountDto> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetEntityByIdAsync(command.CustomerId);
            if (customer is not null)
            {
                var account = _mapper.Map<Entities.Account>(command);
                account.AccountNumber = Helpers.GenerateAccountNumber(customer);
                var response = await _repo.CreateEntityAsync(account);
                return _mapper.Map<AccountDto>(response);
            }
            throw new Exception("Customer not found");
        }
    }
}
