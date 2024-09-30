using BankServices.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServices
{
    public interface IAccountDomain
    {
        string GenerateAccountNumber(Customer customer);
        Account? CreateAccount(Customer customer);
        void DeleteAccount(string accountNumber);
        AccountVM? GetAccount(string accountNumber);
        bool BankAccountExists(string accountNumber);
        ICollection<AccountVM> GetAccounts();
    }
}
