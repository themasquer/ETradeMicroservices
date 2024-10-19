using Products.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Products.Application.Contexts.Bases;

namespace Products.Persistence.Contexts
{
    public class ProductDb : DbContext, IProductDb
	{
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ProductStore> ProductStores { get; set; }

        public ProductDb(DbContextOptions options) : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			#region String Property Maximum Lengths
			modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(200);

			modelBuilder.Entity<Category>().Property(c => c.Name).HasMaxLength(100);

			modelBuilder.Entity<Store>().Property(c => c.Name).HasMaxLength(150);
			#endregion

			#region Relationships
			modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products).OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<ProductStore>().HasOne(ps => ps.Product).WithMany(p => p.ProductStores).OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<ProductStore>().HasOne(ps => ps.Store).WithMany(s => s.ProductStores).OnDelete(DeleteBehavior.NoAction);
			#endregion

			#region Unique Indices
			modelBuilder.Entity<ProductStore>().HasIndex(ps => new { ps.ProductId, ps.StoreId }).IsUnique();
			#endregion
		}
	}
}
