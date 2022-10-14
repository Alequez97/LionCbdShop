﻿using AutoMapper;
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
            .AfterMap<ProductDtoAction>();

        CreateMap<CreateProductRequest, Product>()
            .ForMember(product =>
                product.Name,
                config => config.MapFrom(request => request.ProductName));

        CreateMap<UpdateProductRequest, Product>()
            .ForMember(product =>
                product.Name,
                config => config.MapFrom(request => request.ProductName));
    }
}