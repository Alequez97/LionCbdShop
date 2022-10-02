using AutoMapper;
using LionCbdShop.Domain.Dto;
using LionCbdShop.Persistence.Entities;
using Microsoft.Extensions.Configuration;

namespace LionCbdShop.Domain.Mapper.Actions;

public class ProductDtoAction : IMappingAction<Product, ProductDto>
{
    private readonly IConfiguration _configuration;

    public ProductDtoAction(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public void Process(Product product, ProductDto productDto, ResolutionContext context)
    {
        var imageUrlBase = _configuration["AzureStorage:ProductImagesUrlBase"];
        productDto.Image = $"{imageUrlBase}/{product.ImageName}";
    }
}
