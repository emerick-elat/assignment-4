using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Seeding
{
    internal static class SeedAccount
    {
        internal static void Seed(ModelBuilder builder, IConfig config)
        {
            builder.Entity<Account>()
                .HasData(new List<Account>() { 
                    new Account (config.SystemAccountNumber, 1) 
                });
        }
    }
}
