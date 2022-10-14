using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LionCbdShop.Persistence
{
    internal class LionCbdShopDbContextFactory : IDesignTimeDbContextFactory<LionCbdShopDbContext>
    {
        public LionCbdShopDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LionCbdShopDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=lion-cbd-shop-db;Trusted_Connection=True");

            return new LionCbdShopDbContext(optionsBuilder.Options);
        }
    }
}
