using AutoMapper;
using Infrastructure.BankAccountRepository.Contract;
using MediatR;

namespace Bank.UseCases.Account.QueryGetAccounts
{
    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, ICollection<AccountDto>>
    {
        private readonly IBankAccountRepository _repo;
        public readonly IMapper _mapper;
        public GetAccountsQueryHandler(IBankAccountRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ICollection<AccountDto>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _repo.GetAllAsync();
            var accountsDto = _mapper.Map<ICollection<AccountDto>>(accounts);
            return accountsDto;
        }
    }
}
