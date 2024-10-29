using Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.ScheduledPayment.CommandCreateScheduledPayment
{
    public class CreateScheduledPaymentCommand : IRequest<Entities.ScheduledPayment>
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartOfValidityDate { get; set; }
        public DateTime EndOfValidityDate { get; set; }
        public int Periodicity { get; set; }
    }
}
