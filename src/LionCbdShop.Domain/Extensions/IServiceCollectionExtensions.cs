using LionCbdShop.Domain.Interfaces;
using LionCbdShop.Domain.Mapper.Profiles;
using LionCbdShop.Domain.Services;
using LionCbdShop.Persistence;
using LionCbdShop.Persistence.Interfaces;
using LionCbdShop.Persistence.Repositories.Data;
using LionCbdShop.Persistence.Repositories.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LionCbdShop.Domain.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainLevelServices(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            
            services.AddAutoMapper(typeof(ProductProfile));
            
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICustomerRepository, SqlCustomerRepository>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductRepository, SqlProductRepository>();
            services.AddTransient<IProductImagesRepository, AzureBlobStorageProductImagesRepository>();

            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderRepository, SqlOrderRepository>();

            return services;
        }
    }
}
