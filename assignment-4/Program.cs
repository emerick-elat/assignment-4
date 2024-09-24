using assignment_4.Views;
using BankServices;
using Microsoft.Extensions.DependencyInjection;

namespace assignment_4
{
    public class Program
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
