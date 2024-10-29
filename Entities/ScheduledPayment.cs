using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ScheduledPayment
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartOfValidityDate { get; set; }
        public DateTime EndOfValidityDate { get; set; }
        public int Periodicity {  get; set; }
        public List<ScheduledPaymentItem> ScheduledPaymentItems { get; set; }
    }

    public class ScheduledPaymentItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public int ScheduledPaymentId { get; set; }
        public ScheduledPayment ScheduledPayment { get; set; }
    }
}
