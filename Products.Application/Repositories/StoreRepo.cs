using Products.Application.Contexts.Bases;
using Products.Domain.Entities;
using Core.Repositories.Bases;

namespace Products.Application.Repositories
{
    public class StoreRepo : RepoBase<Store>
    {
        public StoreRepo(IProductDb db) : base(db)
        {
        }
    }
}
