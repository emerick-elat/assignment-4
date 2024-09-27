using BankServices.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServices
{
    public interface IBank
    {
        string GenerateAccountNumber(Customer customer);
        Account? CreateAccount(Customer customer);
        bool DepositMoney(string accountNumber, decimal anmount);
        bool WithdrawMoney(string accountNumber, decimal anmount);
        bool TransferMoney(string SourceAccountId, string DestinationaAccountId, decimal amount);
        void DeleteAccount(string accountNumber);
        AccountVM? GetAccount(string accountNumber);
        bool BankAccountExists(string accountNumber);
        List<AccountVM> GetAccounts();
        List<Transaction> GetTransactionsHistory(string? accountNumber = null, DateRange? range = null);
    }
}
