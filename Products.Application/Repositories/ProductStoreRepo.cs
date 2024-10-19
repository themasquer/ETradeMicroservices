using Products.Application.Contexts.Bases;
using Products.Domain.Entities;
using Core.Repositories.Bases;

namespace Products.Application.Repositories
{
    public class ProductStoreRepo : RepoBase<ProductStore>
    {
        public ProductStoreRepo(IProductDb db) : base(db)
        {
        }
    }
}
