using Customers.Persistence.Seeds;
using Microsoft.AspNetCore.Mvc;

namespace Customers.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DbController : ControllerBase
	{
		[HttpGet("Seed")]
		public IActionResult Seed()
		{
			new SeedCustomerDb().Initialize();
			return Ok("Database seed successful.");
		}
	}
}
