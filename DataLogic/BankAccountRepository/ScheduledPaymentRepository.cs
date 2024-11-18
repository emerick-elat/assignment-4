using Entities;
using Infrastructure.BankAccountRepository.Contract;
using Infrastructure.Context;
using Infrastructure.Generic;

namespace Infrastructure.BankAccountRepository
{
    public class ScheduledPaymentRepository : DataRepository<ScheduledPayment>, IScheduledPaymentRepository
    {
        public ScheduledPaymentRepository(BankContext bankContext) : base(bankContext)
        {
        }
    }
}
