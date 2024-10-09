using Bank.UseCases.Transaction.QueryGetTransactions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.AuditAPI.Controllers.V1
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

        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromQuery] GetTransactionsQuery request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
