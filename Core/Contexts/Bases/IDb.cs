using Microsoft.EntityFrameworkCore;

namespace Core.Contexts.Bases
{
	public interface IDb : IDisposable
	{
		public DbSet<TEntity> Set<TEntity>() where TEntity : class;
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
