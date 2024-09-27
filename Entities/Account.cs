using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public List<Transaction>? Transactions { get; set; }

        public Account(string accountNumber)
        {
            AccountNumber = accountNumber;
            Transactions = new List<Transaction>();
        }

        public decimal GetBalance()
        {
            if (Transactions == null)
            {
                return 0;
            }

            decimal totalDeposits = 0;
            decimal totalWithdrawals = 0;

            foreach (Transaction transaction in Transactions)
            {
                if (transaction.Type == TransactionType.Deposit)
                {
                    totalDeposits += transaction.Amount;
                }

                if (transaction.Type == TransactionType.Withdrawal)
                {
                    totalWithdrawals += transaction.Amount;
                }
            }

            return totalDeposits - totalWithdrawals;
        }

        public string GetAccountNumber()
        {
            if (AccountNumber is null)
            {
                return string.Empty;
            }
            else
            {
                int i = 1;
                StringBuilder sb = new StringBuilder();
                foreach (char c in AccountNumber)
                {
                    sb.Append(c);
                    if (i++ % 4 == 0) sb.Append(" ");
                }
                return sb.ToString();
            }
        }

        public string GetAccountNumber2()
        {
            string number = AccountNumber;
            for (int i = 4; i < number.Length; i += 5)
            {
                number = number.Insert(i, " ");
            }
            return number;
        }
    }

    public class AccountVM(string accountNumber) : Account(accountNumber)
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? FullName { get => $"{FirstName} {LastName}"; }
    }
}
