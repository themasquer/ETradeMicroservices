using Core.Contexts.Bases;
using Core.Entities.Bases;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.Bases
{
    public abstract class RepoBase<TEntity> : IDisposable where TEntity : Entity, new()
	{
		protected readonly IDb _db;

		protected RepoBase(IDb db)
		{
			_db = db;
		}

		public virtual IQueryable<TEntity> Query(bool isNoTracking = false) 
		{
			var query = _db.Set<TEntity>().Where(q => !q.IsDeleted);
			return isNoTracking ? query.AsNoTracking() : query;
		}

		public virtual void Create(TEntity entity)
		{
			_db.Set<TEntity>().Add(entity);
		}

		public virtual void Update(TEntity entity)
		{
			_db.Set<TEntity>().Update(entity);
		}

		public virtual void Delete(int id)
		{
			var entity = Query().SingleOrDefault(e => e.Id == id);
			if (entity is not null)
				_db.Set<TEntity>().Remove(entity);
		}

		public virtual void SoftDelete(int id)
		{
			var entity = Query().SingleOrDefault(e => e.Id == id);
			if (entity is not null)
			{
				entity.IsDeleted = true;
				Update(entity);
			}
		}

        public void Dispose()
        {
			_db?.Dispose();
			GC.SuppressFinalize(this);
        }
    }
}
