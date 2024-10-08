using DataLogic.BankAccountRepository.Contract;
using DataLogic.Context;
using DataLogic.Generic;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.BankAccountRepository
{
    public class BankAccountRepository : DataRepository<Account>, IBankAccountRepository
    {
        public BankAccountRepository(BankContext bankContext, DbSet<Account> dbSet) : base(bankContext, dbSet)
        {
        }
    }
}
