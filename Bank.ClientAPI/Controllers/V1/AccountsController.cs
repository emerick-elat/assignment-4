using Bank.UseCases.Account.CommandCreateAccount;
using Bank.UseCases.Account.QueryGetAccount;
using Bank.UseCases.Account.QueryGetAccounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bank.ClientAPI.Controllers.V1
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<AccountsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetAccountsQuery());
            return Ok(response);
        }

        // GET api/<AccountsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string accountNumber)
        {
            var response = await _mediator.Send(new GetAccountQuery() { AccountNumber = accountNumber });
            if (response == null) { 
                return NotFound();
            }
            else
            {
                return Ok(response);
            }
        }

        // POST api/<AccountsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAccountCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
