using Products.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Products.Persistence.Contexts;
using System.Globalization;
using Core.Contexts.Bases;

namespace Products.Persistence.Seeds
{
	public class SeedProductDb
	{
		public void Initialize()
		{
			var dbFactory = new ProductDbFactory();
			var db = dbFactory.CreateDbContext([]);

			var productStores = db.ProductStores.ToList();
			var stores = db.Stores.ToList();
			var products = db.Products.ToList();
			var categories = db.Categories.ToList();

			db.ProductStores.RemoveRange(productStores);
			db.Stores.RemoveRange(stores);
			db.Products.RemoveRange(products);
			db.Categories.RemoveRange(categories);

			if (productStores.Count > 0)
				db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('ProductStores', RESEED, 0)");
			if (stores.Count > 0)
				db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('Stores', RESEED, 0)");
			if (products.Count > 0)
				db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('Products', RESEED, 0)");
			if (categories.Count > 0)
				db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('Categories', RESEED, 0)");

			db.Stores.Add(new Store()
			{
				Name = "Hepsiburada",
				IsVirtual = true
			});
			db.Stores.Add(new Store()
			{
				Name = "Vatan",
				IsVirtual = false
			});

			db.SaveChanges();

			db.Categories.Add(new Category()
			{
				Name = "Computer",
				Description = "Laptops, desktops and computer peripherals",
				Products = new List<Product>()
				{
					new Product()
					{
						Name = "Laptop",
						UnitPrice = 3000.5m,
						ExpirationDate = new DateTime(2032, 1, 27),
						StockAmount = 10,
						ProductStores = new List<ProductStore>()
						{
							new ProductStore()
							{
								StoreId = db.Stores.SingleOrDefault(s => s.Name == "Hepsiburada").Id
							}
						}
					},
					new Product()
					{
						Name = "Mouse",
						UnitPrice = 20.5m,
						StockAmount = 50,
						ProductStores = new List<ProductStore>()
						{
							new ProductStore()
							{
								StoreId = db.Stores.SingleOrDefault(s => s.Name == "Hepsiburada").Id
							},
							new ProductStore()
							{
								StoreId = db.Stores.SingleOrDefault(s => s.Name == "Vatan").Id
							}
						}
					},
					new Product()
					{
						Name = "Keyboard",
						UnitPrice = 40,
						StockAmount = 45,
						ProductStores = new List<ProductStore>()
						{
							new ProductStore()
							{
								StoreId = db.Stores.SingleOrDefault(s => s.Name == "Hepsiburada").Id
							},
							new ProductStore()
							{
								StoreId = db.Stores.SingleOrDefault(s => s.Name == "Vatan").Id
							}
						}
					},
					new Product()
					{
						Name = "Monitor",
						UnitPrice = 2500,
						ExpirationDate = DateTime.Parse("05/19/2027", new CultureInfo("en-US")),
						StockAmount = 20,
						ProductStores = new List<ProductStore>()
						{
							new ProductStore()
							{
								StoreId = db.Stores.SingleOrDefault(s => s.Name == "Vatan").Id
							}
						}
					}
				}
			});
			db.Categories.Add(new Category()
			{
				Name = "Home Theater System",
				Products = new List<Product>()
				{
					new Product()
					{
						Name = "Speaker",
						UnitPrice = 2500,
						StockAmount = 70
					},
					new Product()
					{
						Name = "Receiver",
						UnitPrice = 5000,
						StockAmount = 30,
						ProductStores = new List<ProductStore>()
						{
							new ProductStore()
							{
								StoreId = db.Stores.SingleOrDefault(s => s.Name == "Vatan").Id
							}
						}
					},
					new Product()
					{
						Name = "Equalizer",
						UnitPrice = 1000,
						StockAmount = 40,
						ProductStores = new List<ProductStore>()
						{
							new ProductStore()
							{
								StoreId = db.Stores.SingleOrDefault(s => s.Name == "Hepsiburada").Id
							},
							new ProductStore()
							{
								StoreId = db.Stores.SingleOrDefault(s => s.Name == "Vatan").Id
							}
						}
					}
				}
			});

			db.SaveChanges();
		}
	}
}
