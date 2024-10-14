using Infrastructure.BankAccountRepository.Contract;
using Infrastructure.Context;
using Infrastructure.Generic;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BankAccountRepository
{
    public class BankAccountRepository : DataRepository<Account>, IBankAccountRepository
    {
        public BankAccountRepository(BankContext bankContext) : base(bankContext)
        {
        }

        public async Task<Account?> GetBankAccountFromID(string accountNumber)
        {
            if (accountNumber is null)
            {
                throw new ArgumentNullException(nameof(accountNumber));
            }
            var account = await bankContext.Accounts
                .Include(x => x.Customer)
                .Include(a => a.TransactionsFrom)
                .Include(b => b.TransactionsTo)
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
            if (account == null)
            {
                return null;
            }
            return account;
        }
    }
}