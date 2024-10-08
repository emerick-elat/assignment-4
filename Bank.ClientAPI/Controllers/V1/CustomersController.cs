using Bank.UseCases.Customer.CommandCreateCustomer;
using Bank.UseCases.Customer.CommandDeleteCustomer;
using Bank.UseCases.Customer.CommandUpdateCustomer;
using Bank.UseCases.Customer.QueryGetCustomer;
using Bank.UseCases.Customer.QueryGetCustomers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bank.ClientAPI.Controllers.V1
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetCustomersQuery());
            return Ok(response);
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _mediator.Send(new GetCustomerQuery() { CustomerId = id});
            return Ok(response);
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomerCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCustomerCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            await _mediator.Send(command);
            return Ok();
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteCustomerCommand() {  CustomerId = id });
            return Ok();
        }
    }
}
