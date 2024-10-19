using Customers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Customers.Application.Contexts.Bases;

namespace Customers.Persistence.Contexts
{
    public class CustomerDb : DbContext, ICustomerDb
	{
        public DbSet<Customer> Customers { get; set; }

        public CustomerDb(DbContextOptions options) : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			#region String Property Maximum Lengths
			modelBuilder.Entity<Customer>().Property(c => c.Name).HasMaxLength(50);
			modelBuilder.Entity<Customer>().Property(c => c.Surname).HasMaxLength(50);
			modelBuilder.Entity<Customer>().Property(c => c.Phone).HasMaxLength(15);
			modelBuilder.Entity<Customer>().Property(c => c.Email).HasMaxLength(200);
			#endregion

			#region Relationships
			
			#endregion
		}
	}
}
