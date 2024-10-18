using Entities;
using Infrastructure.BankAccountRepository.Contract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class BankRoleStore : IRoleStore<BankRole>
    {
        private readonly IBankRoleRepository _repo;
        public BankRoleStore(IBankRoleRepository repo)
        {
            _repo = repo;
        }

        public async Task<IdentityResult> CreateAsync(BankRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role is null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            var r = await _repo.CreateEntityAsync(role);
            if (r is not null)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(new IdentityError() { Description = "An error occured" });
            }
        }

        public Task<IdentityResult> DeleteAsync(BankRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {   
        }

        public Task<BankRole?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<BankRole?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetNormalizedRoleNameAsync(BankRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(BankRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetRoleNameAsync(BankRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(BankRole role, string? normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(BankRole role, string? roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(BankRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
