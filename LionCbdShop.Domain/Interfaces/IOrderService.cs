using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Requests.Orders;

namespace LionCbdShop.Domain.Interfaces;

public interface IOrderService
{
    Task<Response<IEnumerable<OrderDto>>> GetAllAsync();

    Task<Response<OrderDto>> CreateAsync(CreateOrderRequest request);

    public Task<Response> UpdateOrderStatusAsync(UpdateOrderStatusRequest request);
}
