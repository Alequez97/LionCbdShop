using AutoMapper;
using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Requests.Orders;
using LionCbdShop.Persistence.Entities;

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
                .ForMember(orderDto => orderDto.Status, config => config.MapFrom(order => order.Status.ToString()))
                .ForMember(orderDto => 
                    orderDto.CustomerUsername, 
                    config => config.MapFrom(order => order.Customer.Username));

            CreateMap<CartItem, CartItemDto>();
        }
    }
}
