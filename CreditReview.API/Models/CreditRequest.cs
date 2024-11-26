using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditReview.API.Models
{

    public class CreditRequest
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerEmail { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal AccountBalance { get; set; }
        public DateTime? LastTransactionDate { get; set; }
        public CreditRequestStatus Status { get; set; }
    }

    public class CreditRequestOld
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int CustomerId {  get; set; }
        [Required]
        public string CustomerEmail {  get; set; }
        [Required]
        public decimal CreditAmount {  get; set; }
        [Required]
        public decimal AccountBalance {  get; set; }
        public DateTime? LastTransactionDate { get; set; }
        [Required]
        public CreditRequestStatus Status { get; set; } = CreditRequestStatus.Pending;
    }

    public enum CreditRequestStatus { Pending, Approved, Declined}
}
