namespace Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Account> Accounts { get; set; }

        public Customer(string firstName, string lastName, string? accountNumber = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Accounts = accountNumber is not null
                ? new List<Account>() { new Account(accountNumber) }
                : new List<Account>();
        }
    }
}
