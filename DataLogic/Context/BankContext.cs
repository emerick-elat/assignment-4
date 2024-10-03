﻿using DataLogic.Configurations;
using DataLogic.Seeding;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Context
{
    public class BankContext : DbContext
    {   
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=D://_bankData.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new CustomerConfiguration().Configure(modelBuilder.Entity<Customer>());
            new AccountConfiguration().Configure(modelBuilder.Entity<Account>());
            new TransactionConfiguration().Configure(modelBuilder.Entity<Transaction>());

            SeedCustomers.Seed(modelBuilder);
            SeedAccount.Seed(modelBuilder);
            SeedTransaction.Seed(modelBuilder);
        }
    }
}