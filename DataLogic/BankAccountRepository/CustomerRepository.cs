using Infrastructure.BankAccountRepository.Contract;
using Infrastructure.Context;
using Infrastructure.Generic;
using Infrastructure.Generic.Contract;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.BankAccountRepository
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
