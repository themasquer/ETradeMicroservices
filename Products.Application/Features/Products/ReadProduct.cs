using Products.Application.Common.Handlers.Bases;
using Products.Application.Common.Mappers.Bases;
using Products.Application.Features.Categories;
using AutoMapper.QueryableExtensions;
using Products.Domain.Entities;
using MediatR;
using Core.Records.Bases;
using Core.Repositories.Bases;
using Products.Application.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Products.Application.Features.Products
{
    public record ReadProductRequest : Record, IRequest<IQueryable<ReadProductResponse>>;
    public record ReadProductResponse : Record
    {
        public string Name { get; set; }
        public decimal? UnitPrice { get; set; }
        public int StockAmount { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public ReadCategoryResponse Category { get; set; }
        public string? Stores { get; set; }
    }

    public class ReadProductHandler : AppHandler<Product, ReadProductResponse>, IRequestHandler<ReadProductRequest, IQueryable<ReadProductResponse>>
    {
        public ReadProductHandler(UnitOfWorkBase unitOfWork, RepoBase<Product> repo, AppMapperBase<Product, ReadProductResponse> appMapper) 
            : base(unitOfWork, repo, appMapper)
        {
        }

        public Task<IQueryable<ReadProductResponse>> Handle(ReadProductRequest request, CancellationToken cancellationToken)
        {
            _appMapper.AddQueryProfiles(new ProductQueryProfile(), new CategoryQueryProfile());
            return Task.FromResult(_query.Include(p => p.Category).Include(p => p.ProductStores).ThenInclude(ps => ps.Store)
                .OrderByDescending(p => p.StockAmount).ThenBy(p => p.Name).ProjectTo<ReadProductResponse>(_appMapper.Configuration));
        }
    }
}
