using Products.Application.Common.Handlers.Bases;
using Products.Application.Common.Mappers.Bases;
using Products.Domain.Entities;
using MediatR;
using Core.Records.Bases;
using Core.Repositories.Bases;
using Core.Responses;
using Core.Responses.Bases;

namespace Products.Application.Features.Categories
{
    public record SoftDeleteCategoryRequest : Record, IRequest<Response>;

    public class SoftDeleteCategoryHandler : AppHandler<Category, Response>, IRequestHandler<SoftDeleteCategoryRequest, Response>
    {
        public SoftDeleteCategoryHandler(UnitOfWorkBase unitOfWork, RepoBase<Category> repo, AppMapperBase<Category, Response> appMapper) 
            : base(unitOfWork, repo, appMapper)
        {
        }

        public async Task<Response> Handle(SoftDeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            Category entity = _query.SingleOrDefault(c => c.Id == request.Id);
            if (entity is null)
                return new ErrorResponse("Category not found!");
            _repo.SoftDelete(request.Id);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new SuccessResponse("Category deleted successfully.");
        }
    }
}
