using Autofac;
using BankServices;
using DI;
using Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_4.Views
{
    public static class AccountDetails
    {
        public static void Show(AccountVM? account)
        {
            var container = AutofacConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                IAccountDomain _bank = scope.Resolve<IAccountDomain>()
                   ?? throw new ArgumentNullException(nameof(_bank));
                ITransactionDomain _transaction = scope.Resolve<ITransactionDomain>();

                string? choice, accountTo;
                decimal amount = 0m;
                Console.Clear();
                if (account is not null)
                {

                    Console.WriteLine("Account Details");
                    Console.WriteLine("===============================================================");
                    Console.WriteLine($"Customer Name: {account.FullName}");
                    Console.WriteLine($"Account Number: {account.GetAccountNumber2()}");
                    Console.WriteLine($"Account Balance: {account.GetBalance()} EUR");
                    Console.WriteLine("===============================================================");
                    Console.WriteLine();
                    while (true)
                    {
                        Console.WriteLine("CHOOSE AN OPERATION");
                        Console.WriteLine("1. Deposit");
                        Console.WriteLine("2. Withdrawal");
                        Console.WriteLine("3. Transfer");
                        Console.WriteLine("4. Return");
                        choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "1":
                                Console.WriteLine("Enter the Amount to Deposit");
                                decimal.TryParse(Console.ReadLine(), out amount);
                                _transaction.DepositMoney(account.AccountNumber, amount);
                                break;
                            case "2":
                                Console.WriteLine("Enter the Amount to Withdraw");
                                decimal.TryParse(Console.ReadLine(), out amount);
                                _transaction.WithdrawMoney(account.AccountNumber, amount);
                                break;
                            case "3":
                                Console.WriteLine("Account Number to Transfer to");
                                accountTo = Console.ReadLine();
                                if (accountTo is not null && _bank.BankAccountExists(accountTo))
                                {
                                    Console.WriteLine("Enter the Amount to Transfer");
                                    decimal.TryParse(Console.ReadLine(), out amount);
                                    _transaction.TransferMoney(account.AccountNumber, accountTo, amount);
                                }
                                else
                                {
                                    Console.WriteLine("Sorry this bank account do not Exists");
                                }
                                break;
                            case "4":
                                Console.Clear();
                                return;
                            default:
                                Console.WriteLine("Wrong Choice");
                                break;

                        }
                    }
                }
                else
                {
                    Console.WriteLine("Account not found");
                    Console.ReadKey();
                }
            }
            
        }
    }
}
