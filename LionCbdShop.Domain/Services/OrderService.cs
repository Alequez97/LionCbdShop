﻿using AutoMapper;
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
        catch
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
            order.Status = OrderStatus.New;

            await _orderRepository.CreateAsync(order);

            response.IsSuccess = true;
            response.Message = CommonResponseMessage.Create.Success(ResponseMessageEntity.Order);
            response.ResponseObject = _mapper.Map<OrderDto>(order);
         }
        catch
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

    public async Task<Response> UpdateOrderStatusAsync(string orderNumber, OrderStatus status)
    {
        var response = new Response();

        try
        {
            var order = await _orderRepository.GetByOrderNumberAsync(orderNumber);

            if (order == null)
            {
                response.IsSuccess = false;
                response.Message = OrderResponseMessage.NotFound(orderNumber);
                return response;
            }
        }
        catch
        {
            response.IsSuccess = false;
            response.Message = CommonResponseMessage.Update.Error(ResponseMessageEntity.Order);
        }

        return response;
    }
}
