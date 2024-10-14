﻿using Infrastructure.Generic.Contract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BankAccountRepository.Contract
{
    public interface IBankAccountRepository : IDataRepository<Account>
    {
        Task<Account?> GetBankAccountFromID(string accountNumber);
    }
}
