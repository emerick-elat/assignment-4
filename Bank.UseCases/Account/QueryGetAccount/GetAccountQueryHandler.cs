using AutoMapper;
using Bank.UseCases.Account.QueryGetAccounts;
using Infrastructure.BankAccountRepository.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Account.QueryGetAccount
{
    internal class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, AccountDto>
    {
        private readonly IBankAccountRepository _repo;
        public readonly IMapper _mapper;
        public GetAccountQueryHandler(IBankAccountRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<AccountDto> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var account = await _repo.GetBankAccountFromID(request.AccountNumber);
            return _mapper.Map<AccountDto>(account);
        }
    }
}
