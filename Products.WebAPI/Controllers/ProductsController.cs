#nullable disable
using Products.Application.Features.Categories;
using Products.Application.Features.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//Generated from Custom Template.
namespace Products.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new ReadProductRequest());
            var list = await response.ToListAsync();
            return Ok(list);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _mediator.Send(new ReadProductRequest());
            var item = await response.SingleOrDefaultAsync(r => r.Id == id);
            return Ok(item);
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteProductRequest() { Id = id });
            return Ok(response);
        }
    }
}
