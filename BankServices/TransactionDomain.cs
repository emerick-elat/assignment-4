using BankServices.Models;
using DataLogic.Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServices
{
    internal class TransactionDomain : ITransactionDomain
    {

        private readonly IAccountRepository _accountRepo;
        private readonly ITransactionRepository _transactionRepository;
        public TransactionDomain(IAccountRepository accountRepo, ITransactionRepository transactionRepository)
        {
            _accountRepo = accountRepo
                ?? throw new ArgumentNullException(nameof(accountRepo));
            _transactionRepository = transactionRepository
                ?? throw new ArgumentNullException(nameof(transactionRepository));
        }

        public bool DepositMoney(string accountNumber, decimal anmount)
            => CreateTransaction(anmount, TransactionType.Deposit, _DB.SystemAccountNumber, accountNumber);
        public bool WithdrawMoney(string accountNumber, decimal anmount)
            => CreateTransaction(anmount, TransactionType.Withdrawal, accountNumber, _DB.SystemAccountNumber);
        public bool TransferMoney(string SourceAccountId, string DestinationaAccountId, decimal amount)
            => CreateTransaction(amount, TransactionType.Withdrawal, SourceAccountId, DestinationaAccountId);
        public ICollection<Transaction> GetTransactionsHistory(string? accountNumber = null, DateRange? range = null)
        {
            if (accountNumber is not null && !_accountRepo.BankAccountExists(accountNumber))
            {
                Console.WriteLine("Account do not exists");
                throw new ArgumentException("Account do not exists", nameof(accountNumber));
            }
            if (range is null)
            {
                return _transactionRepository.GetTransactionsHistory(accountNumber);
            }
            else
            {
                return _transactionRepository.GetTransactionsHistory(accountNumber, range.Start, range.End);
            }
        }

        private bool CreateTransaction(decimal amount, TransactionType transactionType, string sourceAccountNumber, string? destinationAccoutNumber = null)
        {
            string msg;
            if (amount <= 0)
            {
                msg = "Invalid Amount";
                Console.WriteLine(msg);
                throw new ArgumentException(msg, nameof(amount));
            }

            if (string.IsNullOrEmpty(sourceAccountNumber))
            {
                throw new ArgumentNullException(nameof(sourceAccountNumber));
            }

            sourceAccountNumber = sourceAccountNumber.Trim().Replace(" ", "");

            var sourceAccount = _accountRepo.GetBankAccount(sourceAccountNumber);

            if (sourceAccount is null)
            {
                Console.WriteLine("Transaction Account do not exists");
                throw new ArgumentNullException(nameof(sourceAccount));
            }

            if (sourceAccount.GetBalance() < amount)
            {
                Console.WriteLine("Insuficient funds for the transaction");
                return false;
            }

            if (destinationAccoutNumber is not null)
            {
                destinationAccoutNumber = destinationAccoutNumber.Trim().Replace(" ", "");
                if (sourceAccountNumber.Equals(destinationAccoutNumber))
                {
                    Console.WriteLine("Invalid Operation");
                    throw new ArgumentException("Transaction on the same account", nameof(destinationAccoutNumber));
                }
                var destinationAccount = _accountRepo.GetBankAccount(destinationAccoutNumber);

                if (destinationAccount is null)
                {
                    Console.WriteLine("Transaction destination Account do not exists");
                    throw new ArgumentNullException(nameof(destinationAccount));
                }
            }

            return _transactionRepository.AddTransaction(amount, TransactionType.Withdrawal, sourceAccountNumber, destinationAccoutNumber);
        }
    }
}
