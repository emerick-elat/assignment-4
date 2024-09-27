using assignment_4.Views;
using Audit.Views;
using Autofac;
using BankServices;
using BankServices.Models;
using DI;
using Microsoft.Extensions.DependencyInjection;

namespace Audit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var container = AutofacConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                IBank _bank = scope.Resolve<IBank>()
                    ?? throw new ArgumentNullException(nameof(_bank));

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
