using AutoMapper;
using Customers.Application.Features.Customers;
using Customers.Domain.Entities;

namespace Customers.Application.Mappers
{
	public class CustomerQueryProfile : Profile
	{
        public CustomerQueryProfile()
        {
            CreateMap<Customer, ReadCustomerResponse>()
                .ForMember(c => c.FullName, o => o.MapFrom(s => s.Name + " " + s.Surname));
        }
    }
}
