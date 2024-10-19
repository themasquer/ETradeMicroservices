using Core.Entities.Bases;
using Core.Repositories.Bases;

namespace Core.Handlers.Bases
{
    public abstract class Handler<TEntity> : IDisposable where TEntity : Entity, new()
	{
        protected readonly UnitOfWorkBase _unitOfWork;
        protected readonly RepoBase<TEntity> _repo;
		protected IQueryable<TEntity> _query;

        protected Handler(UnitOfWorkBase unitOfWork, RepoBase<TEntity> repo)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
            _query = _repo.Query();
        }

        public void Dispose()
		{
            _repo.Dispose();
            _unitOfWork.Dispose();
            GC.SuppressFinalize(this);
		}
	}
}
