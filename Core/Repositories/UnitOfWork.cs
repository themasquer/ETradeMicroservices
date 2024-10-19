using Core.Contexts.Bases;
using Core.Repositories.Bases;

namespace Core.Repositories
{
    public class UnitOfWork : UnitOfWorkBase
    {
        public UnitOfWork(IDb db) : base(db)
        {
        }
    }
}
