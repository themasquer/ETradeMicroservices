using Customers.Application.Common.Handlers.Bases;
using Customers.Application.Common.Mappers.Bases;
using Customers.Domain.Entities;
using FluentValidation;
using MediatR;
using Core.Records.Bases;
using Core.Repositories.Bases;
using Core.Responses;
using Core.Responses.Bases;

namespace Customers.Application.Features.Customers
{
	public record UpdateCustomerRequest : Record, IRequest<Response>
    {
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
	};

    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerRequest>
    {
        public UpdateCustomerValidator()
        {
			RuleFor(c => c.Name)
				.NotNull()
				.MinimumLength(2)
				.MaximumLength(50);
			RuleFor(c => c.Surname)
				.NotNull()
				.MinimumLength(2)
				.MaximumLength(50);
			RuleFor(c => c.Phone)
				.NotNull()
				.MinimumLength(10)
				.MaximumLength(15);
			RuleFor(c => c.Email)
				.NotNull()
				.EmailAddress()
				.MaximumLength(200);
			RuleFor(c => c.Address)
				.NotNull();
		}
    }

    public class UpdateCustomerHandler : AppHandler<Customer, Response>, IRequestHandler<UpdateCustomerRequest, Response>
    {
        public UpdateCustomerHandler(UnitOfWorkBase unitOfWork, RepoBase<Customer> repo, AppMapperBase<Customer, Response> appMapper) 
			: base(unitOfWork, repo, appMapper)
        {
        }

        public async Task<Response> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            if (_query.Any(c => c.Id != request.Id && c.Name.ToLower() == request.Name.ToLower().Trim() && c.Surname.ToLower() == request.Surname.ToLower().Trim()))
                return new ErrorResponse("Customer with the same name and surname exists!");
            Customer entity = _appMapper.Map(request);
            _repo.Update(entity);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new SuccessResponse("Customer updated successfully.", entity.Id);
        }
    }
}
