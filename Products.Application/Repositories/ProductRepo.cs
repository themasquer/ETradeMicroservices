using Products.Application.Contexts.Bases;
using Products.Domain.Entities;
using Core.Repositories.Bases;

namespace Products.Application.Repositories
{
    public class ProductRepo : RepoBase<Product>
    {
        public ProductRepo(IProductDb db) : base(db)
        {
        }
    }
}
