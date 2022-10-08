using Azure.Core;
using LionCbdShop.Persistence.Entities;
using LionCbdShop.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LionCbdShop.Persistence.Repositories.Data
{
    public class SqlCustomerRepository : ICustomerRepository
    {
        private readonly LionCbdShopDbContext _dbContext;

        public SqlCustomerRepository(LionCbdShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> GetByUsernameAsync(string username)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(customer => customer.Username == username);
        }

        public async Task CreateAsync(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CustomerProvider> GetCustomerProviderAsync(string name)
        {
            return await _dbContext.CustomerProviders.FirstOrDefaultAsync(cp => cp.Name == name);
        }
    }
}
