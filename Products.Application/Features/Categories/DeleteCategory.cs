using Products.Application.Common.Handlers.Bases;
using Products.Application.Common.Mappers.Bases;
using Products.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Core.Records.Bases;
using Core.Repositories.Bases;
using Core.Responses;
using Core.Responses.Bases;

namespace Products.Application.Features.Categories
{
    public record DeleteCategoryRequest : Record, IRequest<Response>;
    
    public class DeleteCategoryHandler : AppHandler<Category, Response>, IRequestHandler<DeleteCategoryRequest, Response>
    {
        public DeleteCategoryHandler(UnitOfWorkBase unitOfWork, RepoBase<Category> repo, AppMapperBase<Category, Response> appMapper) 
            : base(unitOfWork, repo, appMapper)
        {
        }

        public async Task<Response> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            Category entity = _query.Include(c => c.Products).SingleOrDefault(c =>  c.Id == request.Id);
            if (entity is null)
                return new ErrorResponse("Category not found!");
            if (entity.Products is not null && entity.Products.Any())
                return new ErrorResponse("Category can't be deleted because category has relational products!");
            _repo.Delete(request.Id);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new SuccessResponse("Category deleted successfully.");
        }
    }
}
