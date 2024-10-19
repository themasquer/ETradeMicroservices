using Products.Application.Common.Handlers.Bases;
using Products.Application.Common.Mappers.Bases;
using Products.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Core.Records.Bases;
using Core.Repositories.Bases;
using Core.Responses;
using Core.Responses.Bases;

namespace Products.Application.Features.Stores
{
    public record DeleteStoreRequest : Record, IRequest<Response>;
    
    public class DeleteStoreHandler : AppHandler<Store, Response>, IRequestHandler<DeleteStoreRequest, Response>
    {
        private readonly RepoBase<ProductStore> _productStoreRepo;

		public DeleteStoreHandler(UnitOfWorkBase unitOfWork, RepoBase<Store> repo, AppMapperBase<Store, Response> appMapper, 
            RepoBase<ProductStore> productStoreRepo) : base(unitOfWork, repo, appMapper)
		{
			_productStoreRepo = productStoreRepo;
		}

		public async Task<Response> Handle(DeleteStoreRequest request, CancellationToken cancellationToken)
        {
            Store entity = _query.Include(s => s.ProductStores).SingleOrDefault(s => s.Id == request.Id);
            if (entity is null)
                return new ErrorResponse("Store not found!");
            if (entity.ProductStores is not null && entity.ProductStores.Any())
            {
                foreach (ProductStore productStore in entity.ProductStores)
                {
                    _productStoreRepo.Delete(productStore.Id);
                }
            }
            _repo.Delete(request.Id);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new SuccessResponse("Store deleted successfully.");
        }
    }
}
