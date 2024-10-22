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
    public class CustomerStore : IUserStore<Customer>,
        IUserPasswordStore<Customer>,
        IUserEmailStore<Customer>
    {
        private readonly ICustomerRepository _repo;
        public CustomerStore(ICustomerRepository repository)
        {
            _repo = repository
                ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IdentityResult> CreateAsync(Customer user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                var response = await _repo.CreateEntityAsync(user);
                if (response is not null)
                {
                    return IdentityResult.Success;
                }
                else
                {
                    return IdentityResult.Failed(new IdentityError() { Code = "500", Description = "An Error occured while creating user" });
                }
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError() { Code = ex.StackTrace, Description = ex.Message });
            }
        }

        public async Task<IdentityResult> DeleteAsync(Customer user, CancellationToken cancellationToken)
        {
            await _repo.DeleteEntityAsync(user.Id);
            return IdentityResult.Success;
        }

        public void Dispose()
        {   
        }

        public async Task<Customer?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await _repo.QueryCustomersAsync(c => c.NormalizedEmail == normalizedEmail);
            return result.FirstOrDefault();
        }

        public async Task<Customer?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await _repo.QueryCustomersAsync(c => c.Id.ToString() == userId);
            return result.FirstOrDefault();
        }

        public async Task<Customer?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await _repo.QueryCustomersAsync(c => c.UserName == normalizedUserName);
            return result.FirstOrDefault();
        }

        public async Task<string?> GetEmailAsync(Customer user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return user.Email;
        }

        public async Task<bool> GetEmailConfirmedAsync(Customer user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return user.IsEmailConfirmed;
        }

        public async Task<string?> GetNormalizedEmailAsync(Customer user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return user.NormalizedEmail;
        }

        public async Task<string?> GetNormalizedUserNameAsync(Customer user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return user.NormalizedUserName;
        }

        public async Task<string?> GetPasswordHashAsync(Customer user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return user.EncryptedPassword;
        }

        public async Task<string> GetUserIdAsync(Customer user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return user.Id.ToString();
        }

        public async Task<string?> GetUserNameAsync(Customer user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return user.UserName;
        }

        public async Task<bool> HasPasswordAsync(Customer user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Customer? customer = await _repo.GetEntityByIdAsync(user.Id);
            if (customer == null)
            {
                return false;
            }
            return user.EncryptedPassword is not null;
        }

        public async Task SetEmailAsync(Customer user, string? email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (email == null) throw new ArgumentNullException(nameof(email));
            user.Email = email;
            await _repo.UpdateCustomerAsync(user);
        }

        public async Task SetEmailConfirmedAsync(Customer user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.IsEmailConfirmed = confirmed;
            await _repo.UpdateCustomerAsync(user);
        }

        public async Task SetNormalizedEmailAsync(Customer user, string? normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (normalizedEmail == null) throw new ArgumentNullException(nameof(normalizedEmail));
            user.NormalizedEmail = normalizedEmail;
            await _repo.UpdateCustomerAsync(user);
        }

        public async Task SetNormalizedUserNameAsync(Customer user, string? normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (normalizedName == null) throw new ArgumentNullException(nameof(normalizedName));
            user.NormalizedUserName = normalizedName;
            await _repo.UpdateCustomerAsync(user);
        }

        public async Task SetPasswordHashAsync(Customer user, string? passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (passwordHash == null) throw new ArgumentNullException(nameof(passwordHash));
            user.EncryptedPassword = passwordHash;
            await _repo.UpdateCustomerAsync(user);
        }

        public async Task SetUserNameAsync(Customer user, string? userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (userName == null) throw new ArgumentNullException(nameof(userName));
            user.UserName = userName;
            await _repo.UpdateCustomerAsync(user);
        }

        public async Task<IdentityResult> UpdateAsync(Customer user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            await _repo.UpdateCustomerAsync(user);
            return IdentityResult.Success;
        }
    }
}
