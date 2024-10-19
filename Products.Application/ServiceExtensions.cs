using Products.Domain.Entities;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Core.Repositories;
using Core.Repositories.Bases;
using System.Reflection;
using Products.Application.Common.Mappers.Bases;
using Products.Application.Common.Mappers;
using Products.Application.Repositories;

namespace Products.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddScoped<UnitOfWorkBase, ProductUnitOfWork>();
            services.AddScoped(typeof(RepoBase<Category>), typeof(CategoryRepo));
            services.AddScoped(typeof(RepoBase<Product>), typeof(ProductRepo));
            services.AddScoped(typeof(RepoBase<Store>), typeof(StoreRepo));
            services.AddScoped(typeof(RepoBase<ProductStore>), typeof(ProductStoreRepo));

            services.AddScoped(typeof(AppMapperBase<,>), typeof(AppMapper<,>));

            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = true);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
