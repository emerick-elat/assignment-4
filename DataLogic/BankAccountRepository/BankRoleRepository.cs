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

        public async Task<ICollection<Customer>> GetCustomersInRoleAsync(string roleName)
        {
            BankRole? role = await bankContext.BankRoles
                .Include(x => x.Customers)
                .FirstOrDefaultAsync(x => x.Name == roleName);
            if (role is not null)
            {
                return role.Customers;
            }
            return new List<Customer>();
        }

        public async Task<BankRole?> GetRoleByNameAsync(string roleName)
        {
            BankRole? role = await bankContext.BankRoles.FirstOrDefaultAsync(x => x.Name == roleName);
            return role;
        }
    }
}
