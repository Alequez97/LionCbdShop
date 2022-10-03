using AutoMapper;
using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Interfaces;
using LionCbdShop.Domain.Requests.Orders;

namespace LionCbdShop.Domain.Services;

public class OrderService : IOrderService
{
    private readonly List<OrderDto> _orders = new();
    private readonly IMapper _mapper;

    public OrderService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<Response<IEnumerable<OrderDto>>> GetAllAsync()
    {
        var response = new Response<IEnumerable<OrderDto>>
        {
            IsSuccess = true,
            ResponseObject = _orders
        };

        return Task.FromResult(response);
    }

    public Task<Response> CreateAsync(CreateOrderRequest request)
    {
        var response = new Response
        {
            IsSuccess = true,
            Message = "New order was created"
        };

        var orderDto = _mapper.Map<OrderDto>(request);
        _orders.Add(orderDto);

        return Task.FromResult(response);
    }
}
