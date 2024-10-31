using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CustomerBankRoleConfiguration : IEntityTypeConfiguration<CustomerBankRole>
    {
        public void Configure(EntityTypeBuilder<CustomerBankRole> builder)
        {
            builder.HasKey(cbr => new { cbr.CustomerId, cbr.BankRoleId });

            builder.HasOne<Customer>(sc => sc.Customer)
            .WithMany(s => s.CustomerBankRoles)
            .HasForeignKey(sc => sc.CustomerId);

            builder.HasOne<BankRole>(sc => sc.BankRole)
                .WithMany(s => s.CustomerBankRoles)
                .HasForeignKey(sc => sc.BankRoleId);
        }
    }
}
