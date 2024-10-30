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

        public async Task<IdentityResult> DeleteAsync(BankRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _repo.DeleteEntityAsync(role.Id);
            return IdentityResult.Success;
        }

        public void Dispose()
        {   
        }

        public async Task<BankRole?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _repo.GetEntityByIdAsync(roleId);
        }

        public Task<BankRole?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetNormalizedRoleNameAsync(BankRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role is null)
            {
                throw new ArgumentNullException(nameof (role));
            }

            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(BankRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof (role));
            }
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string?> GetRoleNameAsync(BankRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(BankRole role, string? normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof (role));
            }

            if (normalizedName == null)
            {
                throw new ArgumentNullException (nameof (normalizedName));
            }

            role.NormalizedName = normalizedName;

            return Task.FromResult<object>(normalizedName);
        }

        public Task SetRoleNameAsync(BankRole role, string? roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            if (roleName == null)
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            role.Name = roleName;

            return Task.FromResult<object>(roleName);
        }

        public Task<IdentityResult> UpdateAsync(BankRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
