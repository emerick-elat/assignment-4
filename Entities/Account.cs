using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public List<Transaction>? TransactionsTo { get; set; }
        public List<Transaction>? TransactionsFrom { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public Account(string accountNumber)
        {
            AccountNumber = accountNumber;
            TransactionsTo = new List<Transaction>();
        }
        
        public Account(string accountNumber, int customerId)
        {
            CustomerId = customerId;
            AccountNumber = accountNumber;
            TransactionsTo = new List<Transaction>();
        }
        public Account()
        {
        }
        public decimal GetBalance()
        {
            if (TransactionsTo is null) TransactionsTo = new List<Transaction>();
            if (TransactionsFrom is null) TransactionsFrom = new List<Transaction>();

            decimal totalDeposits = TransactionsTo.Sum(t => t.Amount);
            decimal totalWithdrawals = TransactionsFrom.Sum(t => t.Amount);

            return totalDeposits - totalWithdrawals;
        }
    }

    public class AccountVM
    {
        public AccountVM()
        {
        }

        public AccountVM(string accountId)
        {
            AccountNumber = accountId;
        }

        public string? AccountNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public List<Transaction>? TransactionsTo { get; set; }
        public List<Transaction>? TransactionsFrom { get; set; }
        public string? FullName { get => $"{FirstName} {LastName}"; }

        public decimal GetBalance()
        {
            if (TransactionsTo == null) TransactionsTo = new List<Transaction>();
            if (TransactionsFrom == null) TransactionsFrom = new List<Transaction>();

            decimal totalDeposits = TransactionsTo.Sum(t => t.Amount);
            decimal totalWithdrawals = TransactionsFrom.Sum(t => t.Amount);

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
}
