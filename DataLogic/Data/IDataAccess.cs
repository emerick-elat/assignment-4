using DataLogic.Models;

namespace DataLogic.Data
{
    public interface IDataAccess
    {
        List<AccountVM> GetAllAccounts();
        Account? CreateBankAccount(Customer customer);
        AccountVM? GetBankAccount(string accountId);
        bool BankAccountExists(string accountId);
        void DeleteAccount(string accountId);
        bool AddTransaction(decimal amount, TransactionType type, string SourceAccountId, string? DestinationaAccountId = null);
        List<Transaction> GetTransactionsHistory(string? accountNumber = null,  DateTime? start = null, DateTime? end = null);
    }
}
