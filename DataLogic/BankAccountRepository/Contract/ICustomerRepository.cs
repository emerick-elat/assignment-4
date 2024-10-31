using Infrastructure.Generic.Contract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Infrastructure.BankAccountRepository.Contract
{
    public interface ICustomerRepository : IDataRepository<Customer>
    {
        Task<ICollection<Customer>> GetPaginatedResults(int PageSize, int PageNumber);
        Task<ICollection<Customer>> GetCustomersAsync(int id);
        Task<ICollection<Customer>> QueryCustomersAsync(Expression<Func<Customer, bool>> query);
        Task<Customer?> GetCustomerAsync(int id);
        Task<bool> CustomerExistsAsync(int id);
        Task<Customer?> CreateCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
        Task<ICollection<string>> GetCustomerRolesAsync(int customerId);
    }
}
