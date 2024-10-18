using Infrastructure.BankAccountRepository.Contract;
using Infrastructure.Context;
using Infrastructure.Generic;
using Infrastructure.Generic.Contract;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.BankAccountRepository
{
    public class BankRoleRepository : DataRepository<BankRole>, IBankRoleRepository
    {
        public BankRoleRepository(BankContext bankContext) : base(bankContext)
        {
        }
    }
}
