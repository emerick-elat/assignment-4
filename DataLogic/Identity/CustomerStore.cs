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
    public class CustomerStore : IUserStore<Customer>
    {
        private readonly ICustomerRepository _repo;
        public CustomerStore(ICustomerRepository repository)
        {
            _repo = repository;
        }

        public async Task<IdentityResult> CreateAsync(Customer user, CancellationToken cancellationToken)
        {
            var response = await _repo.CreateEntityAsync(user);
            if (response is not null)
            {
                return new IdentityResult();
            } else
            {
                return IdentityResult.Failed();
            }
            
        }

        public async Task<IdentityResult> DeleteAsync(Customer user, CancellationToken cancellationToken)
        {
            await _repo.DeleteEntityAsync(user);
            return new IdentityResult();
        }

        public void Dispose()
        {   
        }

        public async Task<Customer?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _repo.GetEntityByIdAsync(userId);
        }

        public Task<Customer?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetNormalizedUserNameAsync(Customer user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(Customer user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetUserNameAsync(Customer user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(Customer user, string? normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(Customer user, string? userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(Customer user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
