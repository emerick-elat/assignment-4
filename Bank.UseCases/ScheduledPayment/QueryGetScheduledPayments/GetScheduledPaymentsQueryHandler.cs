using Infrastructure.BankAccountRepository.Contract;
using MediatR;

namespace Bank.UseCases.ScheduledPayment.QueryGetScheduledPayments
{
    internal class GetScheduledPaymentsQueryHandler : IRequestHandler<GetScheduledPaymentsQuery, ICollection<Entities.ScheduledPayment>>
    {
        private readonly IScheduledPaymentRepository _repo;
        public GetScheduledPaymentsQueryHandler(IScheduledPaymentRepository repo)
        {
            _repo = repo;
        }

        public async Task<ICollection<Entities.ScheduledPayment>> Handle(GetScheduledPaymentsQuery request, CancellationToken cancellationToken)
        {
            var records = await _repo.GetAllAsync();
            return records;
        }
    }
}
