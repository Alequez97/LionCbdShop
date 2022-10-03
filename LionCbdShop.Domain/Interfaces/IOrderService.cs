﻿using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Requests.Orders;

namespace LionCbdShop.Domain.Interfaces;

public interface IOrderService
{
    Task<Response<IEnumerable<OrderDto>>> GetAllAsync();

    Task<Response> CreateAsync(CreateOrderRequest request);
}