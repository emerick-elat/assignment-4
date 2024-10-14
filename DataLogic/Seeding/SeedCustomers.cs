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
                new Customer(1, "System", "Account", "admin@smart3bank.com", "+37061224853")
            });
        }
    }
}
