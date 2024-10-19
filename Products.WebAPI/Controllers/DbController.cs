using Microsoft.AspNetCore.Mvc;
using Products.Persistence.Seeds;

namespace Products.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DbController : ControllerBase
	{
		[HttpGet("Seed")]
		public IActionResult Seed()
		{
			new SeedProductDb().Initialize();
			return Ok("Database seed successful.");
		}
	}
}
