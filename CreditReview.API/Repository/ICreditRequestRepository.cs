using CreditReview.API.Models;

namespace CreditReview.API.Repository
{
    public interface ICreditRequestRepository
    {
        Task<ICollection<CreditRequest>> GetAll();
        Task<CreditRequest> GetCreditRequest(int id);
        Task<CreditRequest> CreateCreditRequest(CreditRequest creditRequest);
        Task SetCreditRequestStatus(int creditRequestId, CreditRequestStatus status);
    }
}
