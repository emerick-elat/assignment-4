using Microsoft.EntityFrameworkCore;

namespace CreditReview.API.Models
{
    public class CreditRequestDbContext(DbContextOptions<CreditRequestDbContext> options) : DbContext(options)
    {
        public required DbSet<CreditRequest> CreditRequests { get; set; }
    }
}
