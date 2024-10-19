using Products.Application.Common.Handlers.Bases;
using Products.Application.Common.Mappers.Bases;
using Products.Domain.Entities;
using MediatR;
using Core.Records.Bases;
using Core.Repositories.Bases;
using Core.Responses;
using Core.Responses.Bases;

namespace Products.Application.Features.Stores
{
    public record SoftDeleteStoreRequest : Record, IRequest<Response>;

    public class SoftDeleteStoreHandler : AppHandler<Store, Response>, IRequestHandler<SoftDeleteStoreRequest, Response>
    {
        public SoftDeleteStoreHandler(UnitOfWorkBase unitOfWork, RepoBase<Store> repo, AppMapperBase<Store, Response> appMapper) 
            : base(unitOfWork, repo, appMapper)
        {
        }

        public async Task<Response> Handle(SoftDeleteStoreRequest request, CancellationToken cancellationToken)
        {
            Store entity = _query.SingleOrDefault(s => s.Id == request.Id);
            if (entity is null)
                return new ErrorResponse("Store not found!");
            _repo.SoftDelete(request.Id);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new SuccessResponse("Store deleted successfully.");
        }
    }
}
