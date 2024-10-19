using Products.Application.Common.Handlers.Bases;
using Products.Application.Common.Mappers.Bases;
using Products.Domain.Entities;
using FluentValidation;
using MediatR;
using Core.Records.Bases;
using Core.Repositories.Bases;
using Core.Responses;
using Core.Responses.Bases;
using Products.Application.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Products.Application.Features.Stores
{
    public record UpdateStoreRequest : Record, IRequest<Response>
    {
        public string Name { get; set; }
        public bool IsVirtual { get; set; }
		public List<int> ProductIds { get; set; }
	};

    public class UpdateStoreValidator : AbstractValidator<UpdateStoreRequest>
    {
        public UpdateStoreValidator()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(150);
        }
    }

    public class UpdateStoreHandler : AppHandler<Store, Response>, IRequestHandler<UpdateStoreRequest, Response>
    {
		private readonly RepoBase<ProductStore> _productStoreRepo;

		public UpdateStoreHandler(UnitOfWorkBase unitOfWork, RepoBase<Store> repo, AppMapperBase<Store, Response> appMapper, 
            RepoBase<ProductStore> productStoreRepo) : base(unitOfWork, repo, appMapper)
		{
			_productStoreRepo = productStoreRepo;
		}

		public async Task<Response> Handle(UpdateStoreRequest request, CancellationToken cancellationToken)
        {
            _query = _repo.Query(true);
			if (_query.Any(s => s.Id != request.Id && s.Name.ToLower() == request.Name.ToLower().Trim()))
                return new ErrorResponse("Store with the same name exists!");
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
            entity = _appMapper.Map(request, new StoreCommandProfile());
            _repo.Update(entity);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new SuccessResponse("Store updated successfully.", entity.Id);
        }
    }
}
