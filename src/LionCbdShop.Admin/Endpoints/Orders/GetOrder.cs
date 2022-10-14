namespace LionCbdShop.Admin.Endpoints.Orders;

public class GetOrder : EndpointBaseAsync
    .WithRequest<string>
    .WithActionResult<Response<OrderDto>>
{
    private readonly IOrderService _orderService;

    public GetOrder(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("/api/orders/{id}")]
    [SwaggerOperation(Tags = new[] { SwaggerGroup.Orders })]
    public override async Task<ActionResult<Response<OrderDto>>> HandleAsync([FromRoute]string id, CancellationToken cancellationToken = default)
    {
        var response = await _orderService.GetAsync(new Guid(id));

        return Ok(response);
    }
}