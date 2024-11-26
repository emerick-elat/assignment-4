using CreditReview.API.Models;
using CreditReview.API.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CreditReview.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditRequestsController(ICreditRequestRepository repo) : ControllerBase
    {
        // GET: api/<CreditRequestsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await repo.GetAll();
            return Ok(response);
        }

        // GET api/<CreditRequestsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await repo.GetCreditRequest(id);
            return Ok(response);
        }

        // POST api/<CreditRequestsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreditRequest creditRequest)
        {
            var response = await repo.CreateCreditRequest(creditRequest);
            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        // PUT api/<CreditRequestsController>/5/approve
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> Approve(int id)
        {
            await repo.SetCreditRequestStatus(id, CreditRequestStatus.Approved);
            return Ok();
        }
        
        // PUT api/<CreditRequestsController>/5/approve
        [HttpPut("{id}/decline")]
        public async Task<IActionResult> Decline(int id)
        {
            await repo.SetCreditRequestStatus(id, CreditRequestStatus.Declined);
            return Ok();
        }

        //// DELETE api/<CreditRequestsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
