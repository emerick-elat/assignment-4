using Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public static class IdentityConfiguration
    {
        public static void RunIdentityConfiguration(this ModelBuilder builder)
        {
            builder.Entity<BankCustomer>(entity => {
                entity.ToTable("BankCustomers");
            });

            builder.Entity<BankRole>(entity => {
                entity.ToTable("BankRoles");
            });

            builder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("BankCustomerRoles");
            });

            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("CustomerUserClaims");
            });

            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("CustomerUserLogins");
            });

            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("BankRoleClaims");
            });

            builder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("CustomerUserTokens");
            });
        }
    }
}
