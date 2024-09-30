﻿using Entities;

namespace DataLogic.Data
{
    public interface IAccountRepository
    {
        ICollection<AccountVM> GetAllAccounts();
        Account? CreateBankAccount(Customer customer);
        AccountVM? GetBankAccount(string accountId);
        bool BankAccountExists(string accountId);
        void DeleteAccount(string accountId);
    }
}
