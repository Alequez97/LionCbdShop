﻿using AutoMapper;
using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Requests.Orders;
using LionCbdShop.Persistence.Entities;
using CartItem = LionCbdShop.Persistence.Entities.CartItem;
using CreateOrderRequestCartItem = LionCbdShop.Domain.Requests.Orders.CartItem;

namespace LionCbdShop.Domain.Mapper.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderRequest, Order>();

            CreateMap<CreateOrderRequestCartItem, CartItem>()
                .ForMember(cartItem => cartItem.Product, config => config.Ignore());

            CreateMap<Order, OrderDto>()
                .ForMember(orderDto => orderDto.Status, config => config.MapFrom(order => order.Status.ToString()));

            CreateMap<CartItem, CartItemDto>();
        }
    }
}
