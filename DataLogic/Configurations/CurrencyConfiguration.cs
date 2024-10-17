using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    internal class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(c => c.Code);
            builder.HasAlternateKey(c => c.Symbol);
            builder.Property(c => c.Code).HasMaxLength(3);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(30);
            builder.Property(c => c.ValueToUSD).IsRequired().HasColumnType("decimal(10,5)");
        }
    }
}