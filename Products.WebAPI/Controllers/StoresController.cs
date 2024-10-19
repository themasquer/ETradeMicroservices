#nullable disable
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Application.Features.Stores;

//Generated from Custom Template.
namespace Products.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StoresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Stores
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new ReadStoreRequest());
            var list = await response.ToListAsync();
            return Ok(list);
        }

        // GET: api/Stores/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _mediator.Send(new ReadStoreRequest());
            var item = await response.SingleOrDefaultAsync(r => r.Id == id);
            return Ok(item);
        }

		// POST: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post(CreateStoreRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> Put(UpdateStoreRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Stores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteStoreRequest() { Id = id });
            return Ok(response);
        }
	}
}
