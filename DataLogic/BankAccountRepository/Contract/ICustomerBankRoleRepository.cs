using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BankAccountRepository.Contract
{
    public interface ICustomerBankRoleRepository
    {
        Task<CustomerBankRole> CreateCustomerBankRole(CustomerBankRole customerBankRole);
        Task DeleteCustomerBankRole(int CustomerId, int BankRoleId);
        Task<bool> ExistsCustomerBankRole(int CustomerId, int BankRoleId);
    }
}
