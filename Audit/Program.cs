using assignment_4.Views;
using Audit.Views;
using BankServices;
using BankServices.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Audit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider provider = new ServiceCollection()
                .AddSingleton<IBank, Bank>()
                .BuildServiceProvider();
            IBank _bank = provider.GetService<IBank>()
                ?? throw new ArgumentNullException(nameof(_bank));
            if (_bank is null) return;
            else
            {
                string? choice;

                while (true)
                {
                    Console.WriteLine("WELCOME TO THE BANK MANAGING APP");
                    Console.WriteLine("1. List All Accounts");
                    Console.WriteLine("2. Transactions Journal");

                    choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AccountsList.Show(_bank.GetAccounts());
                            break;
                        case "2":
                            (string? account, DateRange? range) = TransactionLookupForm.Show();
                            Transactions.View(_bank.GetTransactionsHistory(account, range));
                            break;
                        default:
                            Console.WriteLine("Wrong choice");
                            break;
                    }
                }
            }
        }
    }
}
