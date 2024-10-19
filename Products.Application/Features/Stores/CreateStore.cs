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

namespace Products.Application.Features.Stores
{
    public record CreateStoreRequest : Record, IRequest<Response>
    {
        public string Name { get; set; }
        public bool IsVirtual { get; set; }
        public List<int> ProductIds { get; set; }
    };

    public class CreateStoreValidator : AbstractValidator<CreateStoreRequest>
    {
        public CreateStoreValidator()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(150);
        }
    }

    public class CreateStoreHandler : AppHandler<Store, Response>, IRequestHandler<CreateStoreRequest, Response>
    {
        public CreateStoreHandler(UnitOfWorkBase unitOfWork, RepoBase<Store> repo, AppMapperBase<Store, Response> appMapper) 
            : base(unitOfWork, repo, appMapper)
        {
        }

        public async Task<Response> Handle(CreateStoreRequest request, CancellationToken cancellationToken)
        {
            if (_query.Any(s => s.Name.ToLower() == request.Name.ToLower().Trim()))
                return new ErrorResponse("Store with the same name exists!");
            Store entity = _appMapper.Map(request, new StoreCommandProfile());
            _repo.Create(entity);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new SuccessResponse("Store created successfully.", entity.Id);
        }
    }
}
