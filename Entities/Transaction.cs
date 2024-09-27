using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public string? SourceAccountId { get; set; }
        public string? DestinationAccountId { get; set; }
        public string Currency { get; set; }

        public Transaction(int transactionId, decimal amount, string currency, TransactionType type)
        {
            TransactionId = transactionId;
            Amount = amount;
            Type = type;
            TransactionDate = DateTime.Now;
            SourceAccountId = SourceAccountId;
            DestinationAccountId = DestinationAccountId;
            Currency = currency;
        }

        public Transaction(int transactionId, decimal amount, string currency, TransactionType type, string sourceAccountId, string destinationAccountId)
        {
            TransactionId = transactionId;
            Amount = amount;
            Currency = currency;
            Type = type;
            SourceAccountId = sourceAccountId;
            DestinationAccountId = destinationAccountId;
        }

        public Transaction()
        {
        }
    }

    public enum TransactionType { Deposit, Withdrawal }
}
