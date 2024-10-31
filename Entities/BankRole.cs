namespace Entities
{
    public class BankRole
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public List<CustomerBankRole> CustomerBankRoles { get; set; } = [];
        public List<Customer> Customers { get; set; } = [];
    }
}
