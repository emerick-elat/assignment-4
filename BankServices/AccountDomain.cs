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
    internal class AccountDomain : IAccountDomain
    {
        private readonly IAccountRepository _repo;
        private readonly ITransactionRepository _transactionRepository;
        public AccountDomain(IAccountRepository database, ITransactionRepository transactionRepository)
        {
            _repo = database;
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
            if (customer is null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            if (string.IsNullOrWhiteSpace(customer.FirstName)) {
                Console.WriteLine("Firstname is required");
                throw new ArgumentException("The firstname is required", nameof(customer.FirstName));
            }
            customer.Accounts = new List<Account>() { new Account(GenerateAccountNumber(customer)) };
            return _repo.CreateBankAccount(customer);
        }

        public ICollection<AccountVM> GetAccounts()
        {
            return _repo.GetAllAccounts();
        }

        public AccountVM? GetAccount(string accountNumber)
        {
            if (accountNumber is null)
            {
                throw new ArgumentNullException(nameof(accountNumber));
            }

            return _repo.GetBankAccount(accountNumber);
        }

        public bool BankAccountExists(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentNullException($"{nameof(accountNumber)} is null");
            }

            return _repo.BankAccountExists(accountNumber);
        }
            
        public void DeleteAccount(string accountNumber)
            => _repo.DeleteAccount(accountNumber);
    }
}
