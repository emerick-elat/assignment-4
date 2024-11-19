using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context
{
    public class TenantsDBContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Tenant> Tenants { get; set; }

        public TenantsDBContext(DbContextOptions<TenantsDBContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("TenantsDBConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tenant>().HasAlternateKey(t => t.Name);
            modelBuilder.Entity<Tenant>().Property(t => t.Name).HasMaxLength(50);

            modelBuilder.Entity<Tenant>().Property(t => t.Server).IsRequired();
            modelBuilder.Entity<Tenant>().Property(t => t.DatabaseName).IsRequired();
            modelBuilder.Entity<Tenant>().Property(t => t.TrusServerCertificate).HasDefaultValue(true);
            modelBuilder.Entity<Tenant>().Property(t => t.IsTrustedConnection).HasDefaultValue(true);

            modelBuilder.Entity<Tenant>().HasData(new List<Tenant>() {
                new Tenant { Id = 1, Name = "DefaultConnectionString", Server = ".", DatabaseName = "VentionBankDB" },
                new Tenant { Id = 2, Name = "Customer1", Server = "local", DatabaseName = "BankDB1", DBUser = "sa", DBPassword = "#Include <stdio.h>" },
                new Tenant { Id = 3, Name = "Customer2", Server = ".", DatabaseName = "BkDB2", DBUser = "sa", DBPassword = "#Lithu@nia&Baltic$>", TrusServerCertificate = true },
                new Tenant { Id = 4, Name = "Customer3", Server = ".", DatabaseName = "BankDB3", DBUser = "sa", DBPassword = "P@$$w0rd!", TrusServerCertificate = true }
            });
        }
    }
}
