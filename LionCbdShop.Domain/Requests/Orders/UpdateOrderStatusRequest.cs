using LionCbdShop.Persistence.Entities;

namespace LionCbdShop.Domain.Requests.Orders;

public class UpdateOrderStatusRequest
{
    public string OrderNumber { get; set; }

    public OrderStatus Status { get; set; }
}
