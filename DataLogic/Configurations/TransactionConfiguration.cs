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
    internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.TransactionId);
            builder.Property(t => t.TransactionId).ValueGeneratedOnAdd();
            builder.Property(t => t.Amount).IsRequired();
            builder.Property(t => t.TransactionDate).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(t => t.Type).IsRequired();
            builder.Property(t => t.Currency).IsRequired().HasDefaultValue("EUR");
            builder.Property(t => t.SourceAccountId).IsRequired();
            builder.Property(t => t.DestinationAccountId).IsRequired();

            builder.HasOne(t => t.SourceAccount)
                .WithMany(t => t.TransactionsFrom)
                .HasForeignKey(t => t.SourceAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.DestinationAccount)
                .WithMany(t => t.TransactionsTo)
                .HasForeignKey(t => t.DestinationAccountId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
