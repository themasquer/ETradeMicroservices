using Core.Entities.Bases;

namespace Customers.Domain.Entities
{
	public class Customer : Entity
	{
		public string Name { get; set; } = null!;
		public string Surname { get; set; } = null!;
		public string Phone { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string Address { get; set; } = null!;
	}
}
