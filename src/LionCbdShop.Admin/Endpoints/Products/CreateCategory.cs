using LionCbdShop.Domain.Requests.Products;

namespace LionCbdShop.Admin.Endpoints.Products;

public class CreateCategory : EndpointBaseAsync
    .WithRequest<CreateProductCategoryRequest>
    .WithActionResult<Response>
{
    private readonly IProductService _productService;

    public CreateCategory(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("/api/product-categories")]
    [SwaggerOperation(Tags = new[] { SwaggerGroup.Products })]
    public override async Task<ActionResult<Response>> HandleAsync([FromForm]CreateProductCategoryRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _productService.CreateCategoryAsync(request);

        return Ok(response);
    }
}