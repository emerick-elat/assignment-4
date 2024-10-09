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

        public async Task<ICollection<Customer>> GetPaginatedResults(int PageSize, int PageNumber)
        {
            int totalCount = await bankContext.Customers.CountAsync();
            var customers = await bankContext.Customers
                .Skip(PageSize * (PageNumber - 1))
                .Take(PageSize).ToListAsync();
            return customers;
        }
    }
}
