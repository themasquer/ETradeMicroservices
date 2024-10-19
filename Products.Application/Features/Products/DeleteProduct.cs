using Products.Application.Common.Handlers.Bases;
using Products.Application.Common.Mappers.Bases;
using Products.Domain.Entities;
using MediatR;
using Core.Records.Bases;
using Core.Repositories.Bases;
using Core.Responses;
using Core.Responses.Bases;
using Microsoft.EntityFrameworkCore;

namespace Products.Application.Features.Products
{
    public record DeleteProductRequest : Record, IRequest<Response>;
    
    public class DeleteProductHandler : AppHandler<Product, Response>, IRequestHandler<DeleteProductRequest, Response>
    {
		private readonly RepoBase<ProductStore> _productStoreRepo;

		public DeleteProductHandler(UnitOfWorkBase unitOfWork, RepoBase<Product> repo, AppMapperBase<Product, Response> appMapper, 
			RepoBase<ProductStore> productStoreRepo) : base(unitOfWork, repo, appMapper)
		{
			_productStoreRepo = productStoreRepo;
		}

		public async Task<Response> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            Product entity = _query.Include(p => p.ProductStores).SingleOrDefault(p =>  p.Id == request.Id);
            if (entity is null)
                return new ErrorResponse("Product not found!");
			if (entity.ProductStores is not null && entity.ProductStores.Any())
			{
				foreach (ProductStore productStore in entity.ProductStores)
				{
					_productStoreRepo.Delete(productStore.Id);
				}
			}
			_repo.Delete(request.Id);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new SuccessResponse();
        }
    }
}
