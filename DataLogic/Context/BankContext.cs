using DataLogic.Configurations;
using DataLogic.Seeding;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Context
{
    public class BankContext : DbContext, IBankContext
    {
        private readonly IConfig _config;

        public BankContext(IConfig config)
        {
            _config = config;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_config.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new CustomerConfiguration().Configure(modelBuilder.Entity<Customer>());
            new AccountConfiguration().Configure(modelBuilder.Entity<Account>());
            new TransactionConfiguration().Configure(modelBuilder.Entity<Transaction>());

            SeedCustomers.Seed(modelBuilder);
            SeedAccount.Seed(modelBuilder, _config);
            SeedTransaction.Seed(modelBuilder, _config);
        }
    }
}
