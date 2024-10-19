using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Products.Persistence.Contexts;
using Products.Application.Contexts.Bases;

namespace Products.Persistence
{
    public static class ServiceExtensions
	{
		public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<IProductDb, ProductDb>(options => options.UseSqlServer(configuration.GetConnectionString("ProductDb")));
		}
	}
}
