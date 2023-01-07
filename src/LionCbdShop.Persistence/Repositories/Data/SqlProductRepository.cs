using LionCbdShop.Persistence.Entities;
using LionCbdShop.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LionCbdShop.Persistence.Repositories.Data
{
    public class SqlProductRepository : IProductRepository
    {
        private readonly LionCbdShopDbContext _dbContext;

        public SqlProductRepository(LionCbdShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(product => product.Id == id);
        }

        public async Task<ProductCategory> GetCategoryAsync(string name)
        {
            return await _dbContext.ProductCategories.FirstOrDefaultAsync(category => category.Name == name);
        }

        public async Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync()
        {
            return await _dbContext.ProductCategories.ToListAsync();
        }

        public async Task CreateAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task CreateCategoryAsync(ProductCategory productCategory)
        {
            await _dbContext.ProductCategories.AddAsync(productCategory);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _dbContext.Products.FirstAsync(product => product.Id == id);

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(string name)
        {
            var productCategory = await _dbContext.ProductCategories.FirstAsync(category => category.Name == name);

            _dbContext.ProductCategories.Remove(productCategory);
            await _dbContext.SaveChangesAsync();
        }
    }
}
