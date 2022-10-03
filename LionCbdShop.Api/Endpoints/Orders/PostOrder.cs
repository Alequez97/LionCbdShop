using Ardalis.ApiEndpoints;
using LionCbdShop.Api.Constants;
using LionCbdShop.Domain;
using LionCbdShop.Domain.Interfaces;
using LionCbdShop.Domain.Requests.Orders;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LionCbdShop.Api.Endpoints.Products;

public class PostOrder : EndpointBaseAsync
    .WithRequest<CreateOrderRequest>
    .WithActionResult<Response>
{
    private readonly IOrderService _orderService;

    public PostOrder(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("/api/orders")]
    [SwaggerOperation(Tags = new[] { SwaggerGroup.Products })]
    public override async Task<ActionResult<Response>> HandleAsync([FromBody] CreateOrderRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _orderService.CreateAsync(request);

        return Ok(response);
    }
}