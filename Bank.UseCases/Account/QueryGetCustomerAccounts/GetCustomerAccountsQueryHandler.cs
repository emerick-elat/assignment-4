using AutoMapper;
using DataLogic.BankAccountRepository.Contract;
using MediatR;

namespace Bank.UseCases.Account.QueryGetAccounts
{
    public class GetCustomerAccountsQueryHandler : IRequestHandler<GetCustomerAccountsQuery, ICollection<AccountDto>>
    {
        private readonly IBankAccountRepository _repo;
        public readonly IMapper _mapper;
        public GetCustomerAccountsQueryHandler(IBankAccountRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ICollection<AccountDto>> Handle(GetCustomerAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _repo.GetAllAsync();
            var customerAccounts = accounts.Where(a => a.CustomerId == request.CustomerId);
            var accountsDto = _mapper.Map<ICollection<AccountDto>>(customerAccounts);
            return accountsDto;
        }
    }
}
