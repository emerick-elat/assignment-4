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
    public class CustomerBankRoleConfiguration : IEntityTypeConfiguration<CustomerBankRole>
    {
        public void Configure(EntityTypeBuilder<CustomerBankRole> builder)
        {
            builder.HasKey(cbr => new { cbr.CustomerId, cbr.BankRoleId });
        }
    }
}
