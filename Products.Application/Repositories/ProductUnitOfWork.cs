using Core.Repositories.Bases;
using Products.Application.Contexts.Bases;

namespace Products.Application.Repositories
{
    public class ProductUnitOfWork : UnitOfWorkBase
    {
        public ProductUnitOfWork(IProductDb db) : base(db)
        {
        }
    }
}
