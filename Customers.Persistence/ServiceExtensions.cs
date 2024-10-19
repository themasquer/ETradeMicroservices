using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Customers.Persistence.Contexts;
using Customers.Application.Contexts.Bases;

namespace Customers.Persistence
{
    public static class ServiceExtensions
	{
		public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ICustomerDb, CustomerDb>(options => options.UseSqlServer(configuration.GetConnectionString("CustomerDb")));
		}
	}
}
