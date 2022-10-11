namespace LionCbdShop.Admin.Endpoints.Products;

public class DeleteProduct : EndpointBaseAsync
    .WithRequest<string>
    .WithActionResult<Response>
{
    private readonly IProductService _productService;

    public DeleteProduct(IProductService productService)
    {
        _productService = productService;
    }

    [HttpDelete("/api/products/{id}")]
    [SwaggerOperation(Tags = new[] { SwaggerGroup.Products })]
    public override async Task<ActionResult<Response>> HandleAsync([FromRoute]string id, CancellationToken cancellationToken = default)
    {
        var response = await _productService.DeleteAsync(new Guid(id));

        return Ok(response);
    }
}