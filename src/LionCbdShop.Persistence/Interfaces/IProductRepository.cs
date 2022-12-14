using LionCbdShop.Persistence.Entities;

namespace LionCbdShop.Persistence.Interfaces;

public interface IProductRepository
{
    Task<Product> GetAsync(Guid id);

    Task<IEnumerable<Product>> GetAllAsync();

    Task<ProductCategory> GetCategoryAsync(string name);

    Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync();

    Task CreateAsync(Product product);

    Task CreateCategoryAsync(ProductCategory productCategory);

    Task UpdateAsync(Product product);
    
    Task DeleteAsync(Guid id);

    Task DeleteCategoryAsync(string name);
}
