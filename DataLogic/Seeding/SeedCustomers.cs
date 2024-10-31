using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeding
{
    internal static class SeedCustomers
    {
        internal static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(new List<Customer>() {
                new Customer
                {
                    Id = 1,
                    Email = "admin@smartbank.com",
                    UserName = "admin@smartbank.com",
                    NormalizedUserName = "ADMIN",
                    FirstName = "System",
                    LastName = "Account",
                    PhoneNumber = "+37061224853",
                    NormalizedEmail = "ADMIN@SMARTBANK.COM",
                    IsEmailConfirmed = true,
                    EncryptedPassword = "AQAAAAIAAYagAAAAEJSOsPcN9rfn6MOai4xyilTlmaX2Pasz8Gv6VOP1VCIfljAblWVvZvfQD17HNPdk3A=="

                }
            });

            modelBuilder.Entity<BankRole>().HasData(new List<BankRole>()
            {
                new BankRole { Id = 1, Name = "Super Admin", NormalizedName = "SUPER ADMIN", ConcurrencyStamp = "" },
                new BankRole { Id = 2, Name = "Customer", NormalizedName = "CUSTOMER", ConcurrencyStamp = "" },
                new BankRole { Id = 3, Name = "Agent", NormalizedName = "AGENT", ConcurrencyStamp = "" },
            });

            modelBuilder.Entity<CustomerBankRole>().HasData(new List<CustomerBankRole>()
            {
                new CustomerBankRole { CustomerId = 1, BankRoleId = 1 }
            });
        }
    }
}
