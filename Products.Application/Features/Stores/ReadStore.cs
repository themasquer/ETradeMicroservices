using AutoMapper.QueryableExtensions;
using Core.Records.Bases;
using Core.Repositories.Bases;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Products.Application.Common.Handlers.Bases;
using Products.Application.Common.Mappers.Bases;
using Products.Application.Mappers;
using Products.Domain.Entities;

namespace Products.Application.Features.Stores
{
	public record ReadStoreRequest : Record, IRequest<IQueryable<ReadStoreResponse>>;
	public record ReadStoreResponse : Record
	{
        public string Name { get; set; }
        public string IsVirtual { get; set; }
        public List<string> Products { get; set; }
    }

    public class ReadStoreHandler : AppHandler<Store, ReadStoreResponse>, IRequestHandler<ReadStoreRequest, IQueryable<ReadStoreResponse>>
	{
        public ReadStoreHandler(UnitOfWorkBase unitOfWork, RepoBase<Store> repo, AppMapperBase<Store, ReadStoreResponse> appMapper) 
            : base(unitOfWork, repo, appMapper)
        {
        }

        public Task<IQueryable<ReadStoreResponse>> Handle(ReadStoreRequest request, CancellationToken cancellationToken)
		{
           _appMapper.AddQueryProfiles(new StoreQueryProfile());
			return Task.FromResult(_query.Include(s => s.ProductStores).ThenInclude(ps => ps.Product)
                .OrderBy(s => s.IsVirtual).ThenBy(s => s.Name).ProjectTo<ReadStoreResponse>(_appMapper.Configuration));
		}
	}
}
