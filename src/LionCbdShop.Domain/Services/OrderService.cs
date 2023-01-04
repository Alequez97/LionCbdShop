using AutoMapper;
using LionCbdShop.Domain.Constants;
using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Interfaces;
using LionCbdShop.Domain.Requests.Orders;
using LionCbdShop.Persistence.Entities;
using LionCbdShop.Persistence.Interfaces;
using Microsoft.Extensions.Logging;

namespace LionCbdShop.Domain.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly Random _random;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository, IMapper mapper,
        ILogger<OrderService> logger)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _random = new Random();
        _mapper = mapper;
        _logger = logger;
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
            _logger.LogError(exception, "Exception: Unable to get all orders");
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
            _logger.LogError(exception, "Exception: Unable to get order with id - {OrderId}", orderId);
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
            var customer = await _customerRepository.GetByIdInCustomerProviderSystemAsync(request.IdInCustomerProviderSystem);
            order.Customer = customer;
            order.OrderNumber = GenerateOrderNumber();
            order.CreationDate = DateTime.UtcNow;
            order.Status = OrderStatus.New;

            await _orderRepository.CreateAsync(order);

            response.IsSuccess = true;
            response.Message = CommonResponseMessage.Create.Success(ResponseMessageEntity.Order);
            response.ResponseObject = _mapper.Map<OrderDto>(order);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception: Unable to create order");
            response.IsSuccess = false;
            response.Message = CommonResponseMessage.Create.Error(ResponseMessageEntity.Order);
        }

        return response;
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
                order.PaymentDate = DateTime.UtcNow;
            }

            await _orderRepository.UpdateAsync(order);

            response.IsSuccess = true;
            response.Message = OrderResponseMessage.StatusWasUpdated(request.OrderNumber);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"Exception: Unable to update status of order with number {request.OrderNumber}",
                request.OrderNumber);
            response.IsSuccess = false;
            response.Message = CommonResponseMessage.Update.Error(ResponseMessageEntity.Order);
        }

        return response;
    }

    public async Task<Response> UpdateShippingAddressAsync(UpdateShippingAddressRequest request)
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

            order.CountryIso2Code = request.CountryIso2Code;
            order.City = request.City;
            order.StreetLine1 = request.StreetLine1;
            order.StreetLine2 = request.StreetLine2;
            order.PostCode = request.PostCode;

            await _orderRepository.UpdateAsync(order);

            response.IsSuccess = true;
            response.Message = OrderResponseMessage.ShippingAddressWasUpdated(request.OrderNumber);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"Exception: Unable to update status of order with number {request.OrderNumber}",
                request.OrderNumber);
            response.IsSuccess = false;
            response.Message = CommonResponseMessage.Update.Error(ResponseMessageEntity.Order);
        }

        return response;
    }

    private string GenerateOrderNumber()
    {
        return $"{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}-{_random.Next(1000, 9999)}";
    }
}