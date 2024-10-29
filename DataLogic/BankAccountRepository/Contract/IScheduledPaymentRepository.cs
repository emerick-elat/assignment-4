using Entities;
using Infrastructure.Generic.Contract;

namespace Infrastructure.BankAccountRepository.Contract
{
    public interface IScheduledPaymentRepository : IDataRepository<ScheduledPayment>
    {
    }
}
