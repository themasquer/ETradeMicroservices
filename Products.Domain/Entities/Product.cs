using Core.Entities.Bases;

namespace Products.Domain.Entities
{
    public class Product : Entity
	{
		public string Name { get; set; } = null!;
        public decimal? UnitPrice { get; set; }
        public int StockAmount { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<ProductStore>? ProductStores { get; set; }
    }
}
