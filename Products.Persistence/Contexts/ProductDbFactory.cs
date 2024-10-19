using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Products.Persistence.Contexts
{
	public class ProductDbFactory : IDesignTimeDbContextFactory<ProductDb>
	{
		public ProductDb CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ProductDb>();
			optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=CA_CQRS_MS_ETrade_ProductDB;trusted_connection=true;");
			return new ProductDb(optionsBuilder.Options);
		}
	}
}
