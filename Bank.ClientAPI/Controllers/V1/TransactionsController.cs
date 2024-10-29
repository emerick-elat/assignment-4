using Bank.UseCases.ScheduledPayment.CommandExecuteScheduledPayment;
using Bank.UseCases.Transaction;
using Bank.UseCases.Transaction.CommandCreateTransaction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.ClientAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly string SystemAccountNumber;

        public TransactionsController(IMediator mediator,
            IConfiguration configuration)
        {
            _mediator = mediator
                ?? throw new ArgumentNullException(nameof(mediator));
            SystemAccountNumber = configuration["SystemAccountNumber"] 
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("transfert")]
        public async Task<IActionResult> Transfer([FromBody]CreateTransactionCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody]CashModelDto data)
        {
            var response = await _mediator.Send(new ExecuteScheduledPaymentCommand()
            {
                Amount = data.Amount,
                AccountNumber = data.AccountNumber,
            });
            return Ok(response);
        }

        [HttpPost("withdrawal")]
        public async Task<IActionResult> Withdrawal([FromBody]CashModelDto data)
        {
            var response = await _mediator.Send(new CreateTransactionCommand()
            {
                Amount = data.Amount,
                Type = Entities.TransactionType.Deposit,
                SourceAccountId = data.AccountNumber,
                DestinationAccountId = SystemAccountNumber
            });
            return Ok(response);
        }
    }
}
