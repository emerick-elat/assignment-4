using Bank.UseCases.Transaction.CommandCreateTransaction;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.ClientAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Transfer([FromBody]CreateTransactionCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
