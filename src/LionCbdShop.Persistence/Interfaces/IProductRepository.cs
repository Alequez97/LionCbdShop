using LionCbdShop.Persistence.Entities;

namespace LionCbdShop.Persistence.Interfaces;

public interface IProductRepository
{
    Task<Product> GetAsync(Guid id);

    Task<IEnumerable<Product>> GetAllAsync();

    Task<ProductCategory> GetProductCategoryAsync(string name);

    Task CreateAsync(Product product);

    Task UpdateAsync(Product product);
    
    Task DeleteAsync(Guid id);
}
