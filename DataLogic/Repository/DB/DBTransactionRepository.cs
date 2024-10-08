using DataLogic.Context;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataLogic.Repository.DB
{
    internal class DBTransactionRepository : ITransactionRepository
    {
        private readonly BankContext _context;

        public DBTransactionRepository(BankContext context)
        {
            _context = context;
        }

        public bool AddTransaction(decimal amount, TransactionType type, string SourceAccountId, string? DestinationaAccountId = null)
        {
            var transaction = new Transaction
            {
                Amount = amount,
                Type = type,
                SourceAccountId = SourceAccountId,
                DestinationAccountId = DestinationaAccountId,
                Currency = "EUR"
            };
            var response = _context.Transactions.Add(transaction);
            _context.SaveChanges();
            if (response.Entity is not null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ICollection<Transaction> GetTransactionsHistory(string? accountNumber = null, DateTime? start = null, DateTime? end = null)
        {
            List<Transaction> transactions = new List<Transaction>();
            if (accountNumber is not null)
            {
                accountNumber = accountNumber.Replace(" ", "");
                transactions = _context.Transactions.Where(t => t.SourceAccountId == accountNumber || t.DestinationAccountId == accountNumber).ToList();
            }
            else
            {
                transactions = _context.Transactions.ToList();
            }

            if (start is not null && end is not null)
            {
                transactions = transactions.Where(t => t.TransactionDate >= start && t.TransactionDate <= end).ToList();
            }
            return transactions;
        }
    }
}
