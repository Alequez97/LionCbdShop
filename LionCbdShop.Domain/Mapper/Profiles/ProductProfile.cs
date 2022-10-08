using AutoMapper;
using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Mapper.Actions;
using LionCbdShop.Domain.Requests.Products;
using LionCbdShop.Persistence.Entities;

namespace LionCbdShop.Domain.Mapper.Profiles;

public class ProductProfile : AutoMapper.Profile
{
    public ProductProfile()
    {
        //CreateMap<Product, ProductDto>()
        //    .ForMember(productDto =>
        //            productDto.Id,
        //        config => config.MapFrom(product => product.RowKey))
        //    .ForMember(productDto =>
        //        productDto.ProductName,
        //        config => config.MapFrom(product => product.Name))
        //    .AfterMap<ProductDtoAction>();

        //CreateMap<CreateProductRequest, Product>()
        //    .ForMember(product =>
        //        product.Name,
        //        config => config.MapFrom(request => request.ProductName))
        //    .ForMember(product =>
        //        product.Timestamp,
        //        config => config.MapFrom(_ => DateTimeOffset.Now));

        //CreateMap<UpdateProductRequest, Product>()
        //    .ForMember(product =>
        //            product.RowKey,
        //        config => config.MapFrom(request => request.Id))
        //    .ForMember(product =>
        //            product.Name,
        //        config => config.MapFrom(request => request.ProductName))
        //    .ForMember(product =>
        //            product.Timestamp,
        //        config => config.MapFrom(_ => DateTimeOffset.Now));
    }
}
