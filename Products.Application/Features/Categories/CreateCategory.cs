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
    public record CreateCategoryRequest : Record, IRequest<Response>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    };

    public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(c => c.Description)
                .MaximumLength(500);
        }
    }

    public class CreateCategoryHandler : AppHandler<Category, Response>, IRequestHandler<CreateCategoryRequest, Response>
    {
        public CreateCategoryHandler(UnitOfWorkBase unitOfWork, RepoBase<Category> repo, AppMapperBase<Category, Response> appMapper) 
            : base(unitOfWork, repo, appMapper)
        {
        }

        public async Task<Response> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            if (_query.Any(c => c.Name.ToLower() == request.Name.ToLower().Trim()))
                return new ErrorResponse("Category with the same name exists!");
            Category entity = _appMapper.Map(request);
            _repo.Create(entity);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new SuccessResponse("Category created successfully.", entity.Id);
        }
    }
}
