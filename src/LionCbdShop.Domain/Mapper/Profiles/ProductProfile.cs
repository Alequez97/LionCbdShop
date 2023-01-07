using AutoMapper;
using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Mapper.Actions;
using LionCbdShop.Domain.Requests.Products;
using LionCbdShop.Persistence.Entities;

namespace LionCbdShop.Domain.Mapper.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(productDto =>
                productDto.ProductName,
                config => config.MapFrom(product => product.Name))
            .ForMember(productDto =>
                    productDto.ProductCategoryName,
                config => config.MapFrom(product => product.Category.Name))
            .AfterMap<ProductDtoAction>();

        CreateMap<ProductCategory, ProductCategoryDto>();

        CreateMap<CreateProductRequest, Product>()
            .ForMember(product =>
                product.Name,
                config => config.MapFrom(request => request.ProductName));

        CreateMap<CreateProductCategoryRequest, ProductCategory>();

        CreateMap<UpdateProductRequest, Product>()
            .ForMember(product =>
                product.Name,
                config => config.MapFrom(request => request.ProductName));
    }
}
