using BankServices.Models;
using DataLogic.Data;
using Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServices
{
    internal class Bank : IBank
    {
        private readonly IAccountRepository _database;
        private readonly ITransactionRepository _transactionRepository;
        public Bank(IAccountRepository database, ITransactionRepository transactionRepository)
        {
            _database = database;
            _transactionRepository = transactionRepository;
        }

        public string GenerateAccountNumber(Customer customer)
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            sb.Append(customer.FirstName.ToUpper().Substring(0, 2));//2
            sb.Append(DateTime.Now.Month.ToString("00"));//2
            sb.Append(random.Next(0, 9999).ToString("0000"));//4
            sb.Append(DateTime.Now.Year.ToString("0000"));//4
            sb.Append(DateTime.Now.Day.ToString("00"));//2
            sb.Append(customer?.LastName?.ToUpper().Substring(0, 2));//2
            return sb.ToString();
        }
        public Account? CreateAccount(Customer customer)
        {
            customer.Accounts = new List<Account>() { new Account(GenerateAccountNumber(customer)) };
            return _database.CreateBankAccount(customer);
        }

        public List<AccountVM> GetAccounts()
            => _database.GetAllAccounts();
        public AccountVM? GetAccount(string accountNumber)
            => _database.GetBankAccount(accountNumber);
        public bool BankAccountExists(string accountNumber)
            => _database.BankAccountExists(accountNumber);
        public void DeleteAccount(string accountNumber)
            => _database.DeleteAccount(accountNumber);
        public bool DepositMoney(string accountNumber, decimal anmount)
            => _transactionRepository.AddTransaction(anmount, TransactionType.Deposit, accountNumber);
        public bool WithdrawMoney(string accountNumber, decimal anmount)
            => _transactionRepository.AddTransaction(anmount, TransactionType.Withdrawal, accountNumber);
        public bool TransferMoney(string SourceAccountId, string DestinationaAccountId, decimal amount)
            => SourceAccountId.Equals(DestinationaAccountId.Replace(" ", ""))
            ? false
            : _transactionRepository.AddTransaction(amount, TransactionType.Withdrawal, SourceAccountId, DestinationaAccountId);
        public List<Transaction> GetTransactionsHistory(string? accountNumber = null, DateRange? range = null)
            => range is null
            ? _transactionRepository.GetTransactionsHistory(accountNumber)
            : _transactionRepository.GetTransactionsHistory(accountNumber, range.Start, range.End);
    }
}
