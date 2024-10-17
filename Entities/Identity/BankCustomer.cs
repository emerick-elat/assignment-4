using Microsoft.AspNetCore.Identity;

namespace Entities.Identity
{
    public class BankCustomer : IdentityUser<int>
    {
        public string? FirstName {  get; set; }
        public string? LastName { get; set; }
    }
}
