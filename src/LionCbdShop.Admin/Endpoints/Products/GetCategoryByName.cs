namespace LionCbdShop.Admin.Endpoints.Products;

public class GetCategoryByName : EndpointBaseAsync
    .WithRequest<string>
    .WithActionResult<Response<ProductDto>>
{
    private readonly IProductService _productService;

    public GetCategoryByName(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("/api/product-categories/{name}")]
    [SwaggerOperation(Tags = new[] { SwaggerGroup.Products })]
    public override async Task<ActionResult<Response<ProductDto>>> HandleAsync([FromRoute]string name, CancellationToken cancellationToken = default)
    {
        var response = await _productService.GetCategoryAsync(name);

        return Ok(response);
    }
}