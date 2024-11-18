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
            modelBuilder.Entity<Tenant>().Property(t => t.Name).HasMaxLength(20);
            modelBuilder.Entity<Tenant>().HasIndex(t => t.Name).IsUnique();
            modelBuilder.Entity<Tenant>().Property(t => t.Name).HasMaxLength(150);

            modelBuilder.Entity<Tenant>().HasData(new List<Tenant>() { 
                new Tenant { Id = 1, Name = "DefaultConnectionString", DBConnectionString = "Server=.;Database=BankDB;Trusted_Connection=True;TrustServerCertificate=True;" },
                new Tenant { Id = 2, Name = "Customer1", DBConnectionString = "Server=.;Database=BankDB1;Trusted_Connection=True;TrustServerCertificate=True;" },
                new Tenant { Id = 3, Name = "Customer2", DBConnectionString = "Server=.;Database=BankDB2;Trusted_Connection=True;TrustServerCertificate=True;" },
                new Tenant { Id = 4, Name = "Customer3", DBConnectionString = "Server=.;Database=BankDB3;Trusted_Connection=True;TrustServerCertificate=True;" },
            });
        }
    }
}
