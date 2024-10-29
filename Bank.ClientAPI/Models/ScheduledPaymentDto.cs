using Entities;

namespace Bank.ClientAPI.Models
{
    public class ScheduledPaymentDto
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartOfValidityDate { get; set; }
        public DateTime EndOfValidityDate { get; set; }
        public int Periodicity { get; set; }
        public int NumberOfPayments { get; set; }
        public DateTime LastPaymentDate { get; set; }
    }
}
