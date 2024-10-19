using Core.Repositories.Bases;
using Customers.Application.Common.Mappers;
using Customers.Application.Common.Mappers.Bases;
using Customers.Application.Repositories;
using Customers.Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Customers.Application
{
    public static class ServiceExtensions
	{
		public static void ConfigureApplication(this IServiceCollection services)
		{
			services.AddScoped<UnitOfWorkBase, CustomerUnitOfWork>();
			services.AddScoped(typeof(RepoBase<Customer>), typeof(CustomerRepo));

			services.AddScoped(typeof(AppMapperBase<,>), typeof(AppMapper<,>));

			services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

			services.AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = true);
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
