namespace LionCbdShop.Admin.Endpoints.Orders;

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
    [SwaggerOperation(Tags = new[] { SwaggerGroup.Orders })]
    public override async Task<ActionResult<Response<IEnumerable<OrderDto>>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var response = await _orderService.GetAllAsync();

        return Ok(response);
    }
}