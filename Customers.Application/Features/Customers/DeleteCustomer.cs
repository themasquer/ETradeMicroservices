using Customers.Application.Common.Handlers.Bases;
using Customers.Application.Common.Mappers.Bases;
using Customers.Domain.Entities;
using MediatR;
using Core.Records.Bases;
using Core.Repositories.Bases;
using Core.Responses;
using Core.Responses.Bases;

namespace Customers.Application.Features.Customers
{
	public record DeleteCustomerRequest : Record, IRequest<Response>;
    
    public class DeleteCustomerHandler : AppHandler<Customer, Response>, IRequestHandler<DeleteCustomerRequest, Response>
    {
        public DeleteCustomerHandler(UnitOfWorkBase unitOfWork, RepoBase<Customer> repo, AppMapperBase<Customer, Response> appMapper) 
            : base(unitOfWork, repo, appMapper)
        {
        }

        public async Task<Response> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
        {
            Customer entity = _query.SingleOrDefault(c =>  c.Id == request.Id);
            if (entity is null)
                return new ErrorResponse("Customer not found!");
            _repo.Delete(request.Id);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new SuccessResponse("Customer deleted successfully.");
        }
    }
}
