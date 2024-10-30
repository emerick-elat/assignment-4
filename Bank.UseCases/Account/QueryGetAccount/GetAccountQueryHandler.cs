using AutoMapper;
using Bank.UseCases.Account.QueryGetAccounts;
using Entities;
using Infrastructure.BankAccountRepository.Contract;
using Infrastructure.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Account.QueryGetAccount
{
    internal class GetAccountQueryHandler(IBankAccountRepository _repo,
        IMapper _mapper,
        ICurrencyConverter _converter) 
        : IRequestHandler<GetAccountQuery, AccountDto>
    {   
        public async Task<AccountDto> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var account = await _repo.GetBankAccountFromID(request.AccountNumber);
            AccountDto accountDto = new AccountDto();
            if (account is not null)
            {
                accountDto = _mapper.Map<AccountDto>(account);
                ExchangeRate balance = await _converter.ConvertCurrency(account.GetBalance(), "EUR", request.Currency);
                accountDto.BalanceConverted = balance.ToString();
            }
            return accountDto;
        }
    }
}
