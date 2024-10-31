using Entities;
using Infrastructure.BankAccountRepository.Contract;
using Infrastructure.Context;

namespace Infrastructure.BankAccountRepository
{
    internal class CustomerBankRoleRepository(BankContext bankContext)
        : ICustomerBankRoleRepository
    {
        public async Task<CustomerBankRole> CreateCustomerBankRole(CustomerBankRole customerBankRole)
        {
            bankContext.CustomerBankRoles.Add(customerBankRole);
            await bankContext.SaveChangesAsync();
            return customerBankRole;
        }

        public async Task DeleteCustomerBankRole(int CustomerId, int BankRoleId)
        {
            CustomerBankRole? customerBankRole = bankContext.CustomerBankRoles
                .FirstOrDefault(cbr => cbr.CustomerId == CustomerId && cbr.BankRoleId == BankRoleId);
            if (customerBankRole is not null)
            {
                bankContext.Remove(customerBankRole);
                await bankContext.SaveChangesAsync();
            }
        }
    }
}
