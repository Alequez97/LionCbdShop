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
            var product = await _dbContext.Products.FirstAsync(product => product.Id == id);

            return product;
        }

        public async Task CreateAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }
        public Task UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _dbContext.Products.FirstAsync(product => product.Id == id);

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
