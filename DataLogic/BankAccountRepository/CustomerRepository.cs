using DataLogic.BankAccountRepository.Contract;
using DataLogic.Context;
using DataLogic.Generic;
using DataLogic.Generic.Contract;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLogic.BankAccountRepository
{
    public class CustomerRepository : DataRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BankContext bankContext) : base(bankContext)
        {
        }
    }
}
