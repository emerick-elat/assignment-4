using Entities;
using Infrastructure.BankAccountRepository.Contract;
using Infrastructure.Context;
using Infrastructure.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BankAccountRepository
{
    public class ScheduledPaymentRepository : DataRepository<ScheduledPayment>, IScheduledPaymentRepository
    {
        public ScheduledPaymentRepository(BankContext bankContext) : base(bankContext)
        {
        }
    }
}
