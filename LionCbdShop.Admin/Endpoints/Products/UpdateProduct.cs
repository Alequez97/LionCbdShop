using LionCbdShop.Domain.Requests.Products;

namespace LionCbdShop.Admin.Endpoints.Products;

public class UpdateProduct : EndpointBaseAsync
    .WithRequest<UpdateProductRequest>
    .WithActionResult<Response>
{
    private readonly IProductService _productService;

    public UpdateProduct(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPut("/api/products")]
    [SwaggerOperation(Tags = new[] { SwaggerGroup.Products })]
    public override async Task<ActionResult<Response>> HandleAsync(UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _productService.UpdateAsync(request);

        return Ok(response);
    }
}