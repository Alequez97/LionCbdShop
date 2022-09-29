using LionCbdShop.Persistence.Entities;

namespace LionCbdShop.Persistence.Interfaces;

public interface IProductRepository
{
    Task<Product> GetAsync(string id);

    Task<IEnumerable<Product>> GetAllAsync();

    Task CreateAsync(Product product);

    Task UpdateAsync(Product product);
    
    Task DeleteAsync(string id);
}
