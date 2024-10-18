namespace Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool IsEmailConfirmed { get; set; }

        public string? EncryptedPassword { get; set; }
        public List<Account>? Accounts { get; set; }
    }
}
