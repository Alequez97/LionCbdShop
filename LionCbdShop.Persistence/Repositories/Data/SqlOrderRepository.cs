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
        var order = await _dbContext
            .Orders
            .Include(order => order.Customer)
            .Include(order => order.CartItems)
            .ThenInclude(cartItem => cartItem.Product)
            .FirstOrDefaultAsync(order => order.Id == id);

        return order;
    }

    public async Task<Order> GetByOrderNumberAsync(string orderNumber)
    {
        return await _dbContext.Orders.FirstOrDefaultAsync(order => order.OrderNumber == orderNumber);
    }

    public async Task CreateAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync();
    }
}
