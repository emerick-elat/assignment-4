using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.ScheduledPayment.CommandExecuteScheduledPayment
{
    public class ExecuteScheduledPaymentCommand : IRequest<bool>
    {
        public string? AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
