using Entities;
using Infrastructure.BankAccountRepository.Contract;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public partial class CustomerStore(IBankRoleRepository _roleRepository,
        ICustomerRepository _customerRepository,
        ICustomerBankRoleRepository _customerRoleRepository) 
        :   IUserStore<Customer>,
            IUserPasswordStore<Customer>
    {
        public async Task<IdentityResult> CreateAsync(Customer user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                var response = await _customerRepository.CreateEntityAsync(user);
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
            await _customerRepository.DeleteEntityAsync(user.Id);
            return IdentityResult.Success;
        }

        public void Dispose()
        {
        }

        public async Task<Customer?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await _customerRepository.QueryCustomersAsync(c => c.Id.ToString() == userId);
            return result.FirstOrDefault();
        }

        public async Task<Customer?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await _customerRepository.QueryCustomersAsync(c => c.UserName == normalizedUserName);
            return result.FirstOrDefault();
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
            Customer? customer = await _customerRepository.GetEntityByIdAsync(user.Id);
            if (customer == null)
            {
                return false;
            }
            return user.EncryptedPassword is not null;
        }

        public async Task SetNormalizedUserNameAsync(Customer user, string? normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (normalizedName == null) throw new ArgumentNullException(nameof(normalizedName));
            user.NormalizedUserName = normalizedName;
            await _customerRepository.UpdateCustomerAsync(user);
        }

        public async Task SetPasswordHashAsync(Customer user, string? passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (passwordHash == null) throw new ArgumentNullException(nameof(passwordHash));
            user.EncryptedPassword = passwordHash;
            await _customerRepository.UpdateCustomerAsync(user);
        }

        public async Task SetUserNameAsync(Customer user, string? userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (userName == null) throw new ArgumentNullException(nameof(userName));
            user.UserName = userName;
            await _customerRepository.UpdateCustomerAsync(user);
        }

        public async Task<IdentityResult> UpdateAsync(Customer user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            await _customerRepository.UpdateCustomerAsync(user);
            return IdentityResult.Success;
        }
    }

    public partial class CustomerStore : IUserEmailStore<Customer>
    {

        public async Task<Customer?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await _customerRepository.QueryCustomersAsync(c => c.NormalizedEmail == normalizedEmail);
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
        public async Task SetEmailAsync(Customer user, string? email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (email == null) throw new ArgumentNullException(nameof(email));
            user.Email = email;
            await _customerRepository.UpdateCustomerAsync(user);
        }

        public async Task SetEmailConfirmedAsync(Customer user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.IsEmailConfirmed = confirmed;
            await _customerRepository.UpdateCustomerAsync(user);
        }

        public async Task SetNormalizedEmailAsync(Customer user, string? normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (normalizedEmail == null) throw new ArgumentNullException(nameof(normalizedEmail));
            user.NormalizedEmail = normalizedEmail;
            await _customerRepository.UpdateCustomerAsync(user);
        }
    }

    public partial class CustomerStore : IUserRoleStore<Customer>
    {
        public async Task AddToRoleAsync(Customer user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null) throw new ArgumentNullException(nameof(user));
            if (roleName is null) throw new ArgumentNullException(nameof(roleName));
            BankRole? bankRole = await _roleRepository.GetRoleByNameAsync(roleName);
            if (bankRole is not null)
            {
                var response = await _customerRoleRepository.CreateCustomerBankRole(new CustomerBankRole
                {
                    CustomerId = user.Id,
                    BankRoleId = bankRole.Id,
                });
            }
        }

        public async Task<IList<string>> GetRolesAsync(Customer user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var roles = await _customerRepository.GetCustomerRolesAsync(user.Id);
            return roles.ToList();
        }

        public async Task<IList<Customer>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            if (roleName is null)
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            BankRole? role = await _roleRepository.GetRoleByNameAsync(roleName);
            if (role is not null)
            {
                ICollection<Customer> customers = await _roleRepository.GetCustomersInRoleAsync(roleName);
                return customers.ToList();
            }
            return new List<Customer>();
        }

        public async Task<bool> IsInRoleAsync(Customer user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (roleName is null)
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            BankRole? role = await _roleRepository.GetRoleByNameAsync(roleName);

            if (role is not null)
            {
                return await _customerRoleRepository.ExistsCustomerBankRole(user.Id, role.Id);
            }
            return false;
        }

        public async Task RemoveFromRoleAsync(Customer user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (roleName is null)
            {
                throw new ArgumentNullException(nameof (roleName));
            }

            BankRole? role = await _roleRepository.GetRoleByNameAsync (roleName);
            if (role is not null)
            {
                await _customerRoleRepository.DeleteCustomerBankRole(user.Id, role.Id);
            }
        }
    }
}
