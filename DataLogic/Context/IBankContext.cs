using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLogic.Context
{
    public interface IBankContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Transaction> Transactions { get; set; }
    }
}