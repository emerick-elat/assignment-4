using DataLogic.Generic.Contract;
using Entities;

namespace DataLogic.BankAccountRepository.Contract
{
    public interface IBankTransactionRepository : IDataRepository<Transaction>
    {
        Task<bool> CreateTransaction(Transaction t);
        Task<ICollection<Transaction>> GetTransactionsHistory(string? accountNumber = null, DateTime? StartDate = null, DateTime? EndDate = null);
    }
}
