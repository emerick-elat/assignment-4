namespace Entities
{
    public class BankRole
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }
    }
}
