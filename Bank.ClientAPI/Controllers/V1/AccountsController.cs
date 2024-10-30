using Bank.UseCases.Account.CommandCreateAccount;
using Bank.UseCases.Account.QueryGetAccount;
using Bank.UseCases.Account.QueryGetAccounts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bank.ClientAPI.Controllers.V1
{
    [Route("api/customers/{customerId}/accounts")]
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
        public async Task<IActionResult> Get(int customerId)
        {
            var response = await _mediator.Send(new GetCustomerAccountsQuery() { CustomerId = customerId });
            return Ok(response);
        }

        // GET api/<AccountsController>/5
        [HttpGet("{accountNumber}/{currency}")]
        public async Task<IActionResult> Get(string accountNumber, string currency)
        {
            var request = new GetAccountQuery()
            {
                AccountNumber = accountNumber,
                Currency = currency ?? "USD"
            };
            GetAccountQueryValidators v = new GetAccountQueryValidators();
            var result = v.Validate(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var response = await _mediator.Send(request);
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
        public async Task<IActionResult> Post(int customerId, [FromBody] CreateAccountCommand command)
        {
            var response = await _mediator.Send(new CreateAccountCommand() { CustomerId = customerId});
            return Ok(response);
        }
    }
}
