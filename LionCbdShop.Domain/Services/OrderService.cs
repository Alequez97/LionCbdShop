using AutoMapper;
using LionCbdShop.Domain.Constants;
using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Interfaces;
using LionCbdShop.Domain.Requests.Orders;
using LionCbdShop.Persistence.Entities;
using LionCbdShop.Persistence.Interfaces;

namespace LionCbdShop.Domain.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly Random _random;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _random = new Random();
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<OrderDto>>> GetAllAsync()
    {
        var response = new Response<IEnumerable<OrderDto>>();

        try
        {
            var orders = await _orderRepository.GetAllAsync();
            var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);

            response.IsSuccess = true;
            response.ResponseObject = ordersDto;
        }
        catch (Exception exception)
        {
            response.IsSuccess = false;
            response.Message = CommonResponseMessage.Get.Error(ResponseMessageEntity.Order);
        }

        return response;
    }

    public async Task<Response<OrderDto>> GetAsync(Guid orderId)
    {
        var response = new Response<OrderDto>();

        try
        {
            var order = await _orderRepository.GetAsync(orderId);
            var orderDto = _mapper.Map<OrderDto>(order);

            response.IsSuccess = true;
            response.ResponseObject = orderDto;
        }
        catch (Exception exception)
        {
            response.IsSuccess = false;
            response.Message = CommonResponseMessage.Get.Error(ResponseMessageEntity.Order);
        }

        return response;
    }

    public async Task<Response<OrderDto>> CreateAsync(CreateOrderRequest request)
    {
        var response = new Response<OrderDto>();

        try
        {
            var order = _mapper.Map<Order>(request);
            var customer = await _customerRepository.GetByUsernameAsync(request.CustomerUsername);
            order.Customer = customer;
            order.OrderNumber = GenerateOrderNumber();
            order.CreationDate = DateTime.Now;
            order.Status = OrderStatus.New;

            await _orderRepository.CreateAsync(order);

            response.IsSuccess = true;
            response.Message = CommonResponseMessage.Create.Success(ResponseMessageEntity.Order);
            response.ResponseObject = _mapper.Map<OrderDto>(order);
         }
        catch (Exception exception)
        {
            response.IsSuccess = false;
            response.Message = CommonResponseMessage.Create.Error(ResponseMessageEntity.Order);
        }

        return response;
    }

    private string GenerateOrderNumber()
    {
        return $"{DateTime.Now.ToString("yyyyMMddHHmmss")}-{_random.Next(1000, 9999)}";
    }

    public async Task<Response> UpdateOrderStatusAsync(UpdateOrderStatusRequest request)
    {
        var response = new Response();

        try
        {
            var order = await _orderRepository.GetByOrderNumberAsync(request.OrderNumber);

            if (order == null)
            {
                response.IsSuccess = false;
                response.Message = OrderResponseMessage.NotFound(request.OrderNumber);
                return response;
            }

            order.Status = request.Status;
            if (request.Status == OrderStatus.Paid)
            {
                order.PaymentDate = DateTime.Now;
            }

            await _orderRepository.UpdateAsync(order);

            response.IsSuccess = true;
            response.Message = OrderResponseMessage.StatusWasUpdated(request.OrderNumber);
        }
        catch (Exception exception)
        {
            response.IsSuccess = false;
            response.Message = CommonResponseMessage.Update.Error(ResponseMessageEntity.Order);
        }

        return response;
    }
}
