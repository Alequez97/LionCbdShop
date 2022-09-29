using Azure;
using Azure.Data.Tables;
using LionCbdShop.Persistence.Constants;
using LionCbdShop.Persistence.Entities;
using LionCbdShop.Persistence.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LionCbdShop.Persistence.Repositories.Data;

public class AzureCosmosDbProductRepository : IProductRepository
{
    private readonly TableClient _azureTableClient;

    public AzureCosmosDbProductRepository(IConfiguration configuration)
    {
        var tableClientConnectionString = configuration["AzureStorage:ProductDbConnectionString"];

        _azureTableClient = new TableClient(tableClientConnectionString, AzureTableName.Products);
    }

    public async Task<Product> GetAsync(string id)
    {
        var products = _azureTableClient.Query<Product>(product => product.RowKey == id);

        return await Task.FromResult(products.First());
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = _azureTableClient.Query<Product>();

        return products.AsEnumerable();
    }
    
    public async Task CreateAsync(Product product)
    {
        await _azureTableClient.CreateIfNotExistsAsync();
        await _azureTableClient.AddEntityAsync(product);
    }

    public async Task UpdateAsync(Product product)
    {
        await _azureTableClient.UpdateEntityAsync(product, ETag.All);
    }

    public async Task DeleteAsync(string id)
    {
        await _azureTableClient.DeleteEntityAsync(AzureTablePartitionKey.Products, id);
    }

}