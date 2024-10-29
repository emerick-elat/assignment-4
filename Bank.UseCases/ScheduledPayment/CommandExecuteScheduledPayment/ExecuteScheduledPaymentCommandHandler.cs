using Entities;
using Infrastructure.BankAccountRepository.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.ScheduledPayment.CommandExecuteScheduledPayment
{
    internal class ExecuteScheduledPaymentCommandHandler(IBankTransactionRepository _transactionRepo)
        : IRequestHandler<ExecuteScheduledPaymentCommand, bool>
    {
        public async Task<bool> Handle(ExecuteScheduledPaymentCommand request, CancellationToken cancellationToken)
        {
            return await _transactionRepo.SendMoneyAsync(request.AccountNumber!, request.Amount);
        }
    }
}
