using LionCbdShop.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace LionCbdShop.Persistence;

public class LionCbdShopDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductCategory> ProductCategories { get; set; }

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
            .Entity<ProductCategory>()
            .HasIndex(productCategory => productCategory.Name)
            .IsUnique();

        modelBuilder
            .Entity<Order>()
            .HasIndex(product => product.OrderNumber)
            .IsUnique();
    }
}
