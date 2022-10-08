using LionCbdShop.Persistence.Entities;

namespace LionCbdShop.Persistence.Interfaces
{
    public interface ICustomerRepository
    {
        public Task CreateAsync(Customer customer);

        public Task<Customer> GetByUsernameAsync(string id);

        public Task<CustomerProvider> GetCustomerProviderAsync(string name);
    }
}
