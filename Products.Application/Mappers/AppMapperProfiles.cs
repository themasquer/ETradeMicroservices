using AutoMapper;
using Products.Application.Features.Categories;
using Products.Application.Features.Products;
using Products.Application.Features.Stores;
using Products.Domain.Entities;

namespace Products.Application.Mappers
{
	public class CategoryQueryProfile : Profile
    {
        public CategoryQueryProfile()
        {
            CreateMap<Category, ReadCategoryResponse>()
                .ForMember(d => d.Products, o => o.Ignore());
        }
    }

    public class ProductQueryProfile : Profile
    {
        public ProductQueryProfile()
        {
            CreateMap<Product, ReadProductResponse>()
                .ForMember(d => d.Stores, o => o.MapFrom(s => string.Join(", ", s.ProductStores.Select(ps => ps.Store.Name))));
        }
    }

    public class StoreQueryProfile : Profile
    {
        public StoreQueryProfile()
        {
            CreateMap<Store, ReadStoreResponse>()
                .ForMember(d => d.IsVirtual, o => o.MapFrom(s => s.IsVirtual ? "Virtual" : "Real"))
                .ForMember(d => d.Products, o => o.MapFrom(s => s.ProductStores.Select(ps => ps.Product.Name)));
        }
    }

    public class StoreCommandProfile : Profile
    {
        public StoreCommandProfile()
        {
			CreateMap<CreateStoreRequest, Store>()
				.ForMember(d => d.ProductStores, o => o.MapFrom(s => s.ProductIds.Select(pId => new ProductStore() { ProductId = pId })));
			CreateMap<UpdateStoreRequest, Store>()
				.ForMember(d => d.ProductStores, o => o.MapFrom(s => s.ProductIds.Select(pId => new ProductStore() { ProductId = pId })));
		}
    }
}
