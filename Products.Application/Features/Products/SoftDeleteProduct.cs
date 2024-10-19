using Products.Application.Common.Handlers.Bases;
using Products.Application.Common.Mappers.Bases;
using Products.Domain.Entities;
using MediatR;
using Core.Records.Bases;
using Core.Repositories.Bases;
using Core.Responses;
using Core.Responses.Bases;

namespace Products.Application.Features.Products
{
    public record SoftDeleteProductRequest : Record, IRequest<Response>;
    
    public class SoftDeleteProductHandler : AppHandler<Product, Response>, IRequestHandler<SoftDeleteProductRequest, Response>
    {
        public SoftDeleteProductHandler(UnitOfWorkBase unitOfWork, RepoBase<Product> repo, AppMapperBase<Product, Response> appMapper) 
            : base(unitOfWork, repo, appMapper)
        {
        }

        public async Task<Response> Handle(SoftDeleteProductRequest request, CancellationToken cancellationToken)
        {
            Product entity = _query.SingleOrDefault(p =>  p.Id == request.Id);
            if (entity is null)
                return new ErrorResponse("Product not found!");
            _repo.SoftDelete(request.Id);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new SuccessResponse();
        }
    }
}
