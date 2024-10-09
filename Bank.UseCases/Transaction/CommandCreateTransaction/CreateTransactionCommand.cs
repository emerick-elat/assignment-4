using Entities;
using MediatR;

namespace Bank.UseCases.Transaction.CommandCreateTransaction
{
    public class CreateTransactionCommand : IRequest<bool>
    {
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public string? SourceAccountId { get; set; }
        public string? DestinationAccountId { get; set; }
        public string Currency { get; set; }
    }
}
