using Ardalis.ApiEndpoints;
using LionCbdShop.Api.Constants;
using LionCbdShop.Domain;
using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LionCbdShop.Api.Endpoints.Products;

public class GetProducts : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<Response<IEnumerable<ProductDto>>>
{
    private readonly IProductService _productService;

    public GetProducts(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("/api/products")]
    [SwaggerOperation(Tags = new[] { SwaggerGroup.Products })]
    public override async Task<ActionResult<Response<IEnumerable<ProductDto>>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var products = await _productService.GetAllAsync();

        return Ok(products);
    }
}

