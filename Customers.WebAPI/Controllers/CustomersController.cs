#nullable disable
using Customers.Application.Features.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//Generated from Custom Template.
namespace Customers.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new ReadCustomerRequest());
            var list = await response.ToListAsync();
            return Ok(list);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _mediator.Send(new ReadCustomerRequest());
            var item = await response.SingleOrDefaultAsync(r => r.Id == id);
            return Ok(item);
        }

		// POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> Put(UpdateCustomerRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteCustomerRequest() { Id = id });
            return Ok(response);
        }
	}
}
