using DataLogic.BankAccountRepository.Contract;
using DataLogic.Context;
using DataLogic.Generic;
using DataLogic.Generic.Contract;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLogic.BankAccountRepository
{
    public class BankTransactionRepository : DataRepository<Transaction>, IBankTransactionRepository
    {
        public BankTransactionRepository(BankContext bankContext) : base(bankContext)
        {
        }
    }
}
