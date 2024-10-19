using AutoMapper.QueryableExtensions;
using Core.Records.Bases;
using Core.Repositories.Bases;
using Customers.Application.Common.Handlers.Bases;
using Customers.Application.Common.Mappers.Bases;
using Customers.Application.Mappers;
using Customers.Domain.Entities;
using MediatR;

namespace Customers.Application.Features.Customers
{
	public record ReadCustomerRequest : Record, IRequest<IQueryable<ReadCustomerResponse>>;
	public record ReadCustomerResponse : Record
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
        public string FullName { get; set; }
    }

    public class ReadCustomerHandler : AppHandler<Customer, ReadCustomerResponse>, IRequestHandler<ReadCustomerRequest, IQueryable<ReadCustomerResponse>>
	{
        public ReadCustomerHandler(UnitOfWorkBase unitOfWork, RepoBase<Customer> repo, AppMapperBase<Customer, ReadCustomerResponse> appMapper) 
			: base(unitOfWork, repo, appMapper)
        {
        }

        public Task<IQueryable<ReadCustomerResponse>> Handle(ReadCustomerRequest request, CancellationToken cancellationToken)
		{
			_appMapper.AddQueryProfiles(new CustomerQueryProfile());
            return Task.FromResult(_query.OrderBy(c => c.Name).ThenBy(c => c.Surname).ProjectTo<ReadCustomerResponse>(_appMapper.Configuration));
		}
	}
}
