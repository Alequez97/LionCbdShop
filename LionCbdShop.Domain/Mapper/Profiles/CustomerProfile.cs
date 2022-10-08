using AutoMapper;
using LionCbdShop.Domain.Requests.Customers;
using LionCbdShop.Persistence.Entities;

namespace LionCbdShop.Domain.Mapper.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateCustomerRequest, Customer>()
                .ForMember(customer => customer.CustomerProvider, config => config.Ignore());
        }
    }
}
