using Autofac;
using BankClient.Views;
using BankServices;
using DI;
using Microsoft.Extensions.Configuration;

namespace BankClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", true, true)
                .Build();

            var container = AutofacConfig.Configure2(configration);

            using (var scope = container.BeginLifetimeScope())
            {
                IAccountDomain _bank = scope.Resolve<IAccountDomain>()
                    ?? throw new ArgumentNullException(nameof(_bank));
                string? choice;

                while (true)
                {
                    Console.WriteLine("WELCOME TO THE BANK MANAGING APP");
                    Console.WriteLine("1. Create an Account");
                    Console.WriteLine("2. List All Accounts");
                    Console.WriteLine("3. Load an Account");

                    choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            _bank?.CreateAccount(CreateAccountForm.Show());
                            break;
                        case "2":
                            AccountsList.Show(_bank.GetAccounts());
                            break;
                        case "3":
                            AccountDetails.Show(_bank?.GetAccount(SearchAccountForm.Show()));
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
