using Infrastructure.Generic.Contract;
using Entities;

namespace Infrastructure.BankAccountRepository.Contract
{
    public interface IBankTransactionRepository : IDataRepository<Transaction>
    {
        Task<bool> SendMoneyAsync(string accountNumber, decimal amount);
        Task<bool> WithdrawMoneyAsync(string accountNumber, decimal amount);
        Task<bool> CreateTransaction(Transaction t);
        Task<ICollection<Transaction>> GetTransactionsHistory(string? accountNumber = null, DateTime? StartDate = null, DateTime? EndDate = null);
    }
}
