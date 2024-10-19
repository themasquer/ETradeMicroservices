using Core.Repositories.Bases;
using Customers.Application.Contexts.Bases;

namespace Customers.Application.Repositories
{
    public class CustomerUnitOfWork : UnitOfWorkBase
    {
        public CustomerUnitOfWork(ICustomerDb db) : base(db)
        {
        }
    }
}
