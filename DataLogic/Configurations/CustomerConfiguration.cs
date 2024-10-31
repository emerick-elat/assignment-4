using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(p => p.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.NormalizedUserName)
                .HasMaxLength(50);

            builder.Property(p => p.FirstName)
                .HasMaxLength(50);

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(15);
            
            builder.Property(p => p.Email)
                .HasMaxLength(30);
            
            builder.Property(p => p.IsEmailConfirmed)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasMany(c => c.Accounts)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.CustomerBankRoles).WithOne().HasForeignKey(cbr => cbr.CustomerId).IsRequired();
        }
    }
}
