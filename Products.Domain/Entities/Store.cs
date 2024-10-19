using Core.Entities.Bases;

namespace Products.Domain.Entities
{
	public class Store : Entity
	{
		public string Name { get; set; } = null!;
        public bool IsVirtual { get; set; }
        public List<ProductStore>? ProductStores { get; set; }
    }
}
