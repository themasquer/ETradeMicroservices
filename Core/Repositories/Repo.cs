using Core.Contexts.Bases;
using Core.Entities.Bases;
using Core.Repositories.Bases;

namespace Core.Repositories
{
    public class Repo<TEntity> : RepoBase<TEntity> where TEntity : Entity, new()
	{
		public Repo(IDb db) : base(db)
		{
		}
	}
}
