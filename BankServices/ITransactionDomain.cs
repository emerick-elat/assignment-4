using BankServices.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServices
{
    public interface ITransactionDomain
    {
        bool DepositMoney(string accountNumber, decimal anmount);
        bool WithdrawMoney(string accountNumber, decimal anmount);
        bool TransferMoney(string SourceAccountId, string DestinationaAccountId, decimal amount);
        ICollection<Transaction> GetTransactionsHistory(string? accountNumber = null, DateRange? range = null);
    }
}
