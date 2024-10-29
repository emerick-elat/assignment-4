using Entities;
using Infrastructure.Configurations;
using Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context
{
    public class BankContext : DbContext
    {   
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BankRole> BankRoles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ScheduledPayment> ScheduledPayments { get; set; }
        public DbSet<ScheduledPaymentItem> ScheduledPaymentItems { get; set; }

        private readonly IConfiguration _configuration;
        public BankContext(DbContextOptions options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new CustomerConfiguration().Configure(modelBuilder.Entity<Customer>());
            new AccountConfiguration().Configure(modelBuilder.Entity<Account>());
            new TransactionConfiguration().Configure(modelBuilder.Entity<Transaction>());
            new CurrencyConfiguration().Configure(modelBuilder.Entity<Currency>());
            new BankRoleConfiguration().Configure(modelBuilder.Entity<BankRole>());
            new ScheduledPaymentConfiguration().Configure(modelBuilder.Entity<ScheduledPayment>());

            //modelBuilder.RunIdentityConfiguration();
            
            SeedCustomers.Seed(modelBuilder);
            SeedAccount.Seed(modelBuilder);
            SeedTransaction.Seed(modelBuilder);
            SeedCurrencies.Seed(modelBuilder);
        }
    }
}
