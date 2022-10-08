using LionCbdShop.Domain.Requests.Customers;

namespace LionCbdShop.Domain.Interfaces;

public interface ICustomerService
{
    public Task<Response> CreateAsync(CreateCustomerRequest request);
}
