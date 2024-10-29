using AutoMapper;
using Bank.ClientAPI.Models;
using Bank.UseCases.ScheduledPayment.CommandCreateScheduledPayment;
using Bank.UseCases.ScheduledPayment.QueryGetScheduledPayments;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bank.ClientAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduledPaymentsController(IMediator _mediator, IMapper _mapper) 
        : ControllerBase
    {
        // GET: api/<ScheduledPaymentsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetScheduledPaymentsQuery());
            ICollection<ScheduledPaymentDto> payments = _mapper.Map<ICollection<ScheduledPaymentDto>>(response);
            return Ok(payments);
        }

        // POST api/<ScheduledPaymentsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateScheduledPaymentCommand command)
        {
            CreateScheduledPaymentCommandValidator validator = new CreateScheduledPaymentCommandValidator();
            ScheduledPayment payment = _mapper.Map<ScheduledPayment>(command);
            var result = validator.Validate(payment);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var response = await _mediator.Send(payment);
            return Ok();
        }
    }
}
