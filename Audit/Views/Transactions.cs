using DataLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audit.Views
{
    public static class Transactions
    {
        public static void View(List<Transaction> transactions)
        {
            Console.Clear();
            int i = 1;
            decimal totalDeposits = 0m;
            decimal totalWithdrawals = 0m;
            foreach (Transaction transaction in transactions)
            {
                switch (transaction.Type)
                {
                    case TransactionType.Deposit:
                        Console.ForegroundColor = ConsoleColor.Green;
                        totalDeposits += transaction.Amount;
                        Console.WriteLine($"{i}. --> {transaction.TransactionId} | Deposit | {transaction.TransactionDate} | {transaction.Amount} {transaction.Currency}");
                        break;
                    case TransactionType.Withdrawal:
                        Console.ForegroundColor = ConsoleColor.Red;
                        totalWithdrawals += transaction.Amount;
                        Console.WriteLine($"{i}. <-- {transaction.TransactionId} | Withdrawal | {transaction.TransactionDate} | {transaction.Amount} EUR");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"{i}. <-> {transaction.TransactionId} | {transaction.SourceAccountId} | {transaction.DestinationAccountId} | {transaction.TransactionDate} | {transaction.Amount} EUR");
                        break;

                }
                i++;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine($"Total Deposits: {totalDeposits} EUR");
            Console.WriteLine($"Total Withdrawals: {totalWithdrawals} EUR");
            Console.WriteLine($"Balance: {totalDeposits - totalWithdrawals} EUR");

            Console.ReadKey();
        }
    }
}
