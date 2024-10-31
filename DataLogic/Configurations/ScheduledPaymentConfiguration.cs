using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    class ScheduledPaymentConfiguration : IEntityTypeConfiguration<ScheduledPayment>
    {
        public void Configure(EntityTypeBuilder<ScheduledPayment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(sp => sp.Id).ValueGeneratedOnAdd();

            builder.Property(sp => sp.Periodicity).IsRequired();
            builder.Property(sp => sp.AccountNumber).HasMaxLength(16).IsRequired();
            builder.Property(sp => sp.Amount).IsRequired().HasColumnType("decimal(10,5)");

            builder.HasMany(sp => sp.ScheduledPaymentItems)
                .WithOne(spi => spi.ScheduledPayment)
                .HasForeignKey(sp => sp.ScheduledPaymentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
