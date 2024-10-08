using DataLogic.Context;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Repository.DB
{
    internal class DBAccountRepository : IAccountRepository
    {
        private readonly BankContext _context;

        public DBAccountRepository(BankContext context)
        {
            _context = context;
        }

        public bool BankAccountExists(string accountId)
        {
            return _context.Accounts.Find(accountId) != null;
        }

        public Account? CreateBankAccount(Customer customer)
        {
            if (customer.Accounts is not null && customer.Accounts.Any())
            {
                var accountNumber = customer.Accounts.FirstOrDefault()?.AccountNumber;
                _context.Customers.Add(customer);
                var response = _context.SaveChanges();
                return new Account(accountNumber);
            }
            return null;
        }

        public void DeleteAccount(string accountId)
        {
            Account? account = _context.Accounts.Find(accountId);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                _context.SaveChanges();
            }
        }

        public ICollection<AccountVM> GetAllAccounts()
        {
            var accounts = _context.Accounts.Include(a => a.Customer).ToList();
            var accountsVM = accounts.Select(account => new AccountVM()
            {
                AccountNumber = account.AccountNumber,
                FirstName = account.Customer?.FirstName,
                LastName = account.Customer?.LastName
            }).ToList();

            return accountsVM;
        }

        public AccountVM? GetBankAccount(string accountId)
        {
            var account = _context.Accounts
                .Include(a => a.Customer)
                .Include(a => a.TransactionsTo)
                .Include (a => a.TransactionsFrom)
                .Where(a => a.AccountNumber == accountId)
                .FirstOrDefault();
            var a = new AccountVM(accountId);
            if (account is not null)
            {

                a.AccountNumber = account.AccountNumber;
                a.FirstName = account.Customer?.FirstName;
                a.LastName = account.Customer?.LastName;
                a.TransactionsTo = account.TransactionsTo;
                a.TransactionsFrom = account.TransactionsFrom;
                return a;
            }
            return null;
        }
        
    }
}
