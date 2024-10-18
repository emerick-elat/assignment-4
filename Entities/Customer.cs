namespace Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public List<Account>? Accounts { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }
    }
}
