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
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    FirstName = "System",
                    LastName = "Account",
                    PhoneNumber = "+37061224853",
                    NormalizedEmail = "ADMIN@SMARTBANK.COM",
                    IsEmailConfirmed = true,

                }
            });
        }
    }
}
