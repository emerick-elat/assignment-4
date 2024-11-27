using CreditReview.API.Models;
using EventBus.Event;
using EventBus.Interface;
using Microsoft.EntityFrameworkCore;

namespace CreditReview.API.Repository
{
    public class CreditRequestRepository(
        CreditRequestDbContext context,
        ILogger<CreditRequestRepository> logger,
        IEventBus eventBus
        ) : ICreditRequestRepository
    {
        public async Task<CreditRequest> CreateCreditRequest(CreditRequest creditRequest)
        {
            if (creditRequest == null)
            {
                throw new ArgumentNullException(nameof(creditRequest));
            }
            try
            {
                var response = context.CreditRequests.Add(creditRequest);
                await context.SaveChangesAsync();
                return response.Entity;
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
                throw ex;
            }
        }

        public async Task<ICollection<CreditRequest>> GetAll()
        {
            return await context.CreditRequests.ToListAsync();
        }

        public async Task<CreditRequest> GetCreditRequest(int id)
        {
            var response = await context.CreditRequests.FindAsync(id);
            return response!;
        }

        public async Task SetCreditRequestStatus(int creditRequestId, CreditRequestStatus status)
        {
            var request = await context.CreditRequests.FindAsync(creditRequestId);
            if (request == null)
            {
                throw new ArgumentNullException(nameof(creditRequestId));
            }

            request.Status = status;
            context.CreditRequests.Update(request);
            await context.SaveChangesAsync();
            string decision = status == CreditRequestStatus.Approved ? "approved" : "declined";
            string message = $"Dear Customer, your credit request has been {decision}";
            
            await eventBus.PublishAsync(new MailEvent()
            {
                Email = request.CustomerEmail,
                Subject = "Decision on Credit Review",
                Body = message
            });
        }
    }
}
