using Customers.Domain.Entities;
using Customers.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Customers.Persistence.Seeds
{
	public class SeedCustomerDb
	{
		public void Initialize()
		{
			var dbFactory = new CustomerDbFactory();
			var db = dbFactory.CreateDbContext([]);

			var customers = db.Customers.ToList();
			db.Customers.RemoveRange(customers);

			if (customers.Count > 0)
				db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('Customers', RESEED, 0)");

			db.Customers.Add(new Customer()
			{
				Address = "Ankara",
				Email = "cagil@alsac.com",
				Name = "Çağıl",
				Surname = "Alsaç",
				Phone = "905321112233"
			});
			db.Customers.Add(new Customer()
			{
				Address = "İstanbul",
				Email = "luna@alsac.com",
				Name = "Luna",
				Surname = "Alsaç",
				Phone = "905323332211"
			});
			db.Customers.Add(new Customer()
			{
				Address = "İzmir",
				Email = "leo@alsac.com",
				Name = "Leo",
				Surname = "Alsaç",
				Phone = "905329998877"
			});

			db.SaveChanges();
		}
	}
}
