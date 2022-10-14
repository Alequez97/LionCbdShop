namespace LionCbdShop.Admin.Endpoints.Products;

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
        var response = await _productService.GetAllAsync();

        return Ok(response);
    }
}

