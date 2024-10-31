using Entities;
using Infrastructure.BankAccountRepository.Contract;
using Infrastructure.Context;
using Infrastructure.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<Customer?> GetCustomerAsync(int id)
            => await bankContext.Customers.FindAsync(id);

        public async Task<ICollection<Customer>> GetCustomersAsync(int id)
            => await bankContext.Customers.ToListAsync();

        public async Task<ICollection<Customer>> QueryCustomersAsync(Expression<Func<Customer, bool>> query)
            => await bankContext.Customers.Where(query).ToListAsync();

        public async Task<Customer?> CreateCustomerAsync(Customer customer)
        {
            var response = await bankContext.Customers.AddAsync(customer);
            await bankContext.SaveChangesAsync();
            return response.Entity;
        }

        public async Task<bool> CustomerExistsAsync(int id)
        {
            return await bankContext.Customers.AnyAsync(x => x.Id == id);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            Customer? customer = await bankContext.Customers.FindAsync(id);
            if (customer is not null)
            {
                bankContext.Remove(customer);
                await bankContext.SaveChangesAsync();
            }
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            if (customer is null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            Customer? _customer = await bankContext.Customers.FindAsync(customer.Id);
            if (_customer is not null)
            {
                bankContext.Update(customer);
                await bankContext.SaveChangesAsync();
            }
        }

        public async Task<ICollection<string>> GetCustomerRolesAsync(int customerId)
        {
            ICollection<CustomerBankRole> customerRoles = await bankContext.CustomerBankRoles
                .Include(c => c.BankRole)
                .Where(c => c.CustomerId == customerId).ToListAsync();
            return customerRoles.Select(cr => cr.BankRole!.Name).ToList();
        }
    }
}
