using LionCbdShop.Persistence.Constants;
using LionCbdShop.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace LionCbdShop.Persistence;

public class LionCbdShopDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public DbSet<CustomerProvider> CustomerProviders { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<CartItem> CartItems { get; set; }

    public LionCbdShopDbContext(DbContextOptions<LionCbdShopDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Customer>()
            .HasIndex(customer => customer.Username)
            .IsUnique();

        modelBuilder
            .Entity<Product>()
            .HasIndex(product => product.Name)
            .IsUnique();

        modelBuilder
            .Entity<Order>()
            .HasIndex(product => product.OrderNumber)
            .IsUnique();

        modelBuilder
            .Entity<CustomerProvider>()
            .HasIndex(provider => provider.Name)
            .IsUnique();

        modelBuilder
            .Entity<CustomerProvider>()
            .HasData(new CustomerProvider() { Id = Guid.NewGuid(), Name = CustomerProviderName.Telegram });
    }
}
