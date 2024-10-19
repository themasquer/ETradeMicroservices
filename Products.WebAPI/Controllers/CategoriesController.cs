#nullable disable
using Products.Application.Features.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//Generated from Custom Template.
namespace Products.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new ReadCategoryRequest());
            var list = await response.ToListAsync();
            return Ok(list);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _mediator.Send(new ReadCategoryRequest());
            var item = await response.SingleOrDefaultAsync(r => r.Id == id);
            return Ok(item);
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post(CreateCategoryRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> Put(UpdateCategoryRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteCategoryRequest() { Id = id });
            return Ok(response);
        }

        // DELETE: api/Categories/SoftDelete/5
        [HttpDelete("SoftDelete/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var response = await _mediator.Send(new SoftDeleteCategoryRequest() { Id = id });
            return Ok(response);
        }
    }
}
