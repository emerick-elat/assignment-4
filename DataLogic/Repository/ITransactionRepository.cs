using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Repository
{
    public interface ITransactionRepository
    {
        bool AddTransaction(decimal amount, TransactionType type, string SourceAccountId, string? DestinationaAccountId = null);
        ICollection<Transaction> GetTransactionsHistory(string? accountNumber = null, DateTime? start = null, DateTime? end = null);
    }
}
