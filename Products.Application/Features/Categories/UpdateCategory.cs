using Products.Application.Common.Handlers.Bases;
using Products.Application.Common.Mappers.Bases;
using Products.Domain.Entities;
using FluentValidation;
using MediatR;
using Core.Records.Bases;
using Core.Repositories.Bases;
using Core.Responses;
using Core.Responses.Bases;

namespace Products.Application.Features.Categories
{
    public record UpdateCategoryRequest : Record, IRequest<Response>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    };

    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(c => c.Description)
                .MaximumLength(500);
        }
    }

    public class UpdateCategoryHandler : AppHandler<Category, Response>, IRequestHandler<UpdateCategoryRequest, Response>
    {
        public UpdateCategoryHandler(UnitOfWorkBase unitOfWork, RepoBase<Category> repo, AppMapperBase<Category, Response> appMapper) 
            : base(unitOfWork, repo, appMapper)
        {
        }

        public async Task<Response> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            if (_query.Any(c => c.Id != request.Id && c.Name.ToLower() == request.Name.ToLower().Trim()))
                return new ErrorResponse("Category with the same name exists!");
            Category entity = _appMapper.Map(request);
            _repo.Update(entity);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new SuccessResponse("Category updated successfully.", entity.Id);
        }
    }
}
