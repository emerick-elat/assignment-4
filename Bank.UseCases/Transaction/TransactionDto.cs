using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Transaction
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public string TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string? SourceAccountId { get; set; }
        public string? DestinationAccountId { get; set; }
        public string Currency { get; set; }
    }
}
