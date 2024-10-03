using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Seeding
{
    internal static class SeedTransaction
    {
        internal static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().HasData(new List<Transaction> {
                new Transaction { 
                    TransactionId = 1,
                    Currency = "EUR",
                    Amount = 10000m,
                    SourceAccountId = "1111111111111111",
                    DestinationAccountId = "1111111111111111",
                    Type = TransactionType.Deposit,
                }
            });
        }
    }
}
