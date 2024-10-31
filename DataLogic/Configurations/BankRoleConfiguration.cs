using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    internal class BankRoleConfiguration : IEntityTypeConfiguration<BankRole>
    {
        public void Configure(EntityTypeBuilder<BankRole> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            
            builder.HasIndex(r => r.Name).IsUnique();
            builder.Property(r => r.Name)
                .IsRequired().HasMaxLength(50);

            builder.HasIndex(r => r.NormalizedName).IsUnique();
            builder.Property(r => r.NormalizedName)
                .IsRequired().HasMaxLength(50);

            builder.HasMany(r => r.CustomerBankRoles).WithOne().HasForeignKey(cbr => cbr.BankRoleId).IsRequired();

            
        }
    }
}
