using DataLogic.Generic.Contract;
using Entities;

namespace DataLogic.BankAccountRepository.Contract
{
    public interface IBankTransactionRepository : IDataRepository<Transaction>
    {
        Task<bool> DepositMoney(Transaction t);
        Task<bool> WithdrawMoney(Transaction t);
        Task<bool> TransferMoney(Transaction t);
        Task<ICollection<Transaction>> GetTransactionsHistory(string? accountNumber = null, DateTime? StartDate = null, DateTime? EndDate = null);
    }
}
