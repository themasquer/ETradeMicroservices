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
	public record CreateCustomerRequest : Record, IRequest<Response>
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
	};

	public class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
	{
		public CreateCustomerValidator()
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

	public class CreateCustomerHandler : AppHandler<Customer, Response>, IRequestHandler<CreateCustomerRequest, Response>
	{
		public CreateCustomerHandler(UnitOfWorkBase unitOfWork, RepoBase<Customer> repo, AppMapperBase<Customer, Response> appMapper) 
			: base(unitOfWork, repo, appMapper)
		{
		}

		public async Task<Response> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
		{
			if (_query.Any(c => c.Name.ToLower() == request.Name.ToLower().Trim() && c.Surname.ToLower() == request.Surname.ToLower().Trim()))
				return new ErrorResponse("Customer with the same name and surname exists!");
			Customer entity = _appMapper.Map(request);
			_repo.Create(entity);
			await _unitOfWork.SaveAsync(cancellationToken);
			return new SuccessResponse("Customer created successfully.", entity.Id);
		}
	}
}
