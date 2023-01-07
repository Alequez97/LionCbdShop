using Ardalis.ApiEndpoints;
using LionCbdShop.Api.Constants;
using LionCbdShop.Domain;
using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LionCbdShop.Api.Endpoints.Products;

public class GetCategories : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<Response<IEnumerable<ProductCategoryDto>>>
{
    private readonly IProductService _productService;

    public GetCategories(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("/api/product-categories")]
    [SwaggerOperation(Tags = new[] { SwaggerGroup.Products })]
    public override async Task<ActionResult<Response<IEnumerable<ProductCategoryDto>>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _productService.GetAllCategoriesAsync();

        return Ok(categories);
    }
}