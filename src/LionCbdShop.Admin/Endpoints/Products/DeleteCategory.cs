namespace LionCbdShop.Admin.Endpoints.Products;

public class DeleteCategory : EndpointBaseAsync
    .WithRequest<string>
    .WithActionResult<Response>
{
    private readonly IProductService _productService;

    public DeleteCategory(IProductService productService)
    {
        _productService = productService;
    }

    [HttpDelete("/api/product-categories/{name}")]
    [SwaggerOperation(Tags = new[] { SwaggerGroup.Products })]
    public override async Task<ActionResult<Response>> HandleAsync([FromRoute]string name, CancellationToken cancellationToken = default)
    {
        var response = await _productService.DeleteCategoryAsync(name);

        return Ok(response);
    }
}