using Products.Application.Common.Handlers.Bases;
using Products.Application.Common.Mappers.Bases;
using Products.Application.Features.Products;
using AutoMapper.QueryableExtensions;
using Products.Domain.Entities;
using MediatR;
using Core.Records.Bases;
using Core.Repositories.Bases;
using Products.Application.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Products.Application.Features.Categories
{
    public record ReadCategoryRequest : Record, IRequest<IQueryable<ReadCategoryResponse>>;
	public record ReadCategoryResponse : Record
	{
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<ReadProductResponse> Products { get; set; }
    }

    public class ReadCategoryHandler : AppHandler<Category, ReadCategoryResponse>, IRequestHandler<ReadCategoryRequest, IQueryable<ReadCategoryResponse>>
	{
        public ReadCategoryHandler(UnitOfWorkBase unitOfWork, RepoBase<Category> repo, AppMapperBase<Category, ReadCategoryResponse> appMapper) 
            : base(unitOfWork, repo, appMapper)
        {
        }

        public Task<IQueryable<ReadCategoryResponse>> Handle(ReadCategoryRequest request, CancellationToken cancellationToken)
		{
           _appMapper.AddQueryProfiles(new ProductQueryProfile());
			return Task.FromResult(_query.Include(c => c.Products).OrderBy(c => c.Name).ProjectTo<ReadCategoryResponse>(_appMapper.Configuration));
		}
	}
}
