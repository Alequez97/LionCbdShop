using LionCbdShop.Persistence.Entities;
using LionCbdShop.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LionCbdShop.Persistence.Repositories.Data;

public class SqlOrderRepository : IOrderRepository
{
    private readonly LionCbdShopDbContext _dbContext;

    public SqlOrderRepository(LionCbdShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _dbContext.Orders.ToListAsync();
    }

    public async Task<Order> GetAsync(Guid id)
    {
        return await _dbContext.Orders.FirstOrDefaultAsync(order => order.Id == id);
    }

    public async Task CreateAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }
}
