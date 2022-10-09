using LionCbdShop.Persistence.Entities;

namespace LionCbdShop.Persistence.Interfaces
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<Order>> GetAllAsync();

        public Task<Order> GetAsync(Guid id);

        public Task<Order> GetByOrderNumberAsync(string orderNumber);

        public Task CreateAsync(Order order);

        public Task UpdateAsync(Order order);
    }
}
