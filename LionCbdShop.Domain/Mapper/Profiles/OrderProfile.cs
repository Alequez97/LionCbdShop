using AutoMapper;
using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Requests.Orders;

namespace LionCbdShop.Domain.Mapper.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderRequest, OrderDto>();
            CreateMap<CartItem, CartItemDto>();
        }
    }
}
