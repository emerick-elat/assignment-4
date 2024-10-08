using DataLogic.Generic.Contract;
using Entities;

namespace DataLogic.BankAccountRepository.Contract
{
    public interface IBankTransactionRepository : IDataRepository<Transaction>
    {
    }
}
