using Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeding
{
    public class SeedCurrencies
    {
        public static void Run(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(new List<Currency>()
            {
                new Currency { Code = "USD", Name = "US Dollar", Symbol = '$', ValueToUSD = 1},
                new Currency { Code = "EUR", Name = "Euro", Symbol = '€', ValueToUSD = 1.08m},
                new Currency { Code = "GBP", Name = "Pound Sterling", Symbol = '£', ValueToUSD = 1.30m},
                new Currency { Code = "YEN", Name = "Yen", Symbol = '¥', ValueToUSD = 0.0067m},
                new Currency { Code = "RUB", Name = "Ruble", Symbol = '₽', ValueToUSD = 0.0103118m},

            });
        }
    }
}
