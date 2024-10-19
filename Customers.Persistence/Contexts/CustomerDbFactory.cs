using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Customers.Persistence.Contexts
{
	public class CustomerDbFactory : IDesignTimeDbContextFactory<CustomerDb>
	{
		public CustomerDb CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<CustomerDb>();
			optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=CA_CQRS_MS_ETrade_CustomerDB;trusted_connection=true;");
			return new CustomerDb(optionsBuilder.Options);
		}
	}
}
