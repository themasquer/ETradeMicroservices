using Products.Application.Common.Handlers.Bases;
using Products.Application.Common.Mappers.Bases;
using Products.Domain.Entities;
using FluentValidation;
using MediatR;
using Core.Records.Bases;
using Core.Repositories.Bases;
using Core.Responses;
using Core.Responses.Bases;

namespace Products.Application.Features.Products
{
    public record UpdateProductRequest : Record, IRequest<Response>
    {
        public string Name { get; set; }
        public decimal? UnitPrice { get; set; }
        public int StockAmount { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? CategoryId { get; set; }
    }

    public class UpdateProductValiator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductValiator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(200);
            RuleFor(p => p.UnitPrice)
                .GreaterThan(0);
            RuleFor(p => p.StockAmount)
                .GreaterThanOrEqualTo(0);
            RuleFor(p => p.ExpirationDate)
                .GreaterThanOrEqualTo(DateTime.Today);
        }
    }

    public class UpdateProductHandler : AppHandler<Product, Response>, IRequestHandler<UpdateProductRequest, Response>
    {
        public UpdateProductHandler(UnitOfWorkBase unitOfWork, RepoBase<Product> repo, AppMapperBase<Product, Response> appMapper) 
            : base(unitOfWork, repo, appMapper)
        {
        }

        public async Task<Response> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            if (_query.Any(p => p.Id != request.Id && p.Name.ToLower() == request.Name.ToLower().Trim()))
                return new ErrorResponse("Product with the same name exists!");
            Product entity = _appMapper.Map(request);
            _repo.Update(entity);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new SuccessResponse(entity.Id);
        }
    }
}
