using Products.Application.Contexts.Bases;
using Products.Domain.Entities;
using Core.Repositories.Bases;

namespace Products.Application.Repositories
{
    public class CategoryRepo : RepoBase<Category>
    {
        public CategoryRepo(IProductDb db) : base(db)
        {
        }
    }
}
