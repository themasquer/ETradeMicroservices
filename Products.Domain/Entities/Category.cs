using Core.Entities.Bases;

namespace Products.Domain.Entities
{
    public class Category : Entity
	{
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
        public List<Product>? Products { get; set; }
    }
}
