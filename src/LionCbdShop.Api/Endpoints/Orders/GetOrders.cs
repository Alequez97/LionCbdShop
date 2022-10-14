using Ardalis.ApiEndpoints;
using LionCbdShop.Api.Constants;
using LionCbdShop.Domain;
using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LionCbdShop.Api.Endpoints.Orders;

public class GetOrders : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<Response<IEnumerable<OrderDto>>>
{
    private readonly IOrderService _orderService;

    public GetOrders(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("/api/orders")]
    [SwaggerOperation(Tags = new[] { SwaggerGroup.Products })]
    public override async Task<ActionResult<Response<IEnumerable<OrderDto>>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var response = await _orderService.GetAllAsync();

        return Ok(response);
    }
}