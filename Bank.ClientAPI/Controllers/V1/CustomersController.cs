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
            var response = _mediator.Send(new GetCustomersQuery());
            return Ok(response);
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = _mediator.Send(new GetCustomerQuery() { CustomerId = id});
            return Ok(response);
        }

        // POST api/<CustomersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
