using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeding
{
    internal static class SeedAccount
    {
        internal static void Seed(ModelBuilder builder)
        {
            builder.Entity<Account>()
                .HasData(new List<Account>() { 
                    new Account("1111111111111111", 1),
                    new Account("1111111111111110", 1)
                });
        }
    }
}
