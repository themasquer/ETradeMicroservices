using Core.Repositories.Bases;
using Customers.Application.Contexts.Bases;
using Customers.Domain.Entities;

namespace Customers.Application.Repositories
{
    public class CustomerRepo : RepoBase<Customer>
	{
		public CustomerRepo(ICustomerDb db) : base(db)
		{
		}
	}
}
