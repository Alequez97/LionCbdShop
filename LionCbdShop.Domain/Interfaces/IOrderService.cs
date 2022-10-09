using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Requests.Orders;

namespace LionCbdShop.Domain.Interfaces;

public interface IOrderService
{
    public Task<Response<IEnumerable<OrderDto>>> GetAllAsync();

    public Task<Response<OrderDto>> GetAsync(Guid orderId);

    public Task<Response<OrderDto>> CreateAsync(CreateOrderRequest request);

    public Task<Response> UpdateOrderStatusAsync(UpdateOrderStatusRequest request);
}
