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
        internal static void Seed(ModelBuilder modelBuilder, IConfig config)
        {
            modelBuilder.Entity<Transaction>().HasData(new List<Transaction> {
                new Transaction { 
                    TransactionId = 1,
                    Currency = "EUR",
                    Amount = config.InitialBalance,
                    SourceAccountId = config.SystemAccountNumber,
                    DestinationAccountId = config.SystemAccountNumber,
                    Type = TransactionType.Deposit,
                }
            });
        }
    }
}
