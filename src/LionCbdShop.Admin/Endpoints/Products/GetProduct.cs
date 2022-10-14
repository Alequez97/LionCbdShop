namespace LionCbdShop.Admin.Endpoints.Products;

public class GetProduct : EndpointBaseAsync
    .WithRequest<string>
    .WithActionResult<Response<ProductDto>>
{
    private readonly IProductService _productService;

    public GetProduct(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("/api/products/{id}")]
    [SwaggerOperation(Tags = new[] { SwaggerGroup.Products })]
    public override async Task<ActionResult<Response<ProductDto>>> HandleAsync([FromRoute]string id, CancellationToken cancellationToken = default)
    {
        var response = await _productService.GetAsync(new Guid(id));

        return Ok(response);
    }
}