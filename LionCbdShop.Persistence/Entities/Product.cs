using Azure;
using Azure.Data.Tables;
using LionCbdShop.Persistence.Constants;

namespace LionCbdShop.Persistence.Entities;

public class Product : ITableEntity
{
    public string? Name { get; set; }

    public double OriginalPrice { get; set; }

    public double PriceWithDiscount { get; set; }

    public string? ImageName { get; set; }

    public string PartitionKey { get; set; } = AzureTablePartitionKey.Products;

    public string RowKey { get; set; } = Guid.NewGuid().ToString();
    
    public DateTimeOffset? Timestamp { get; set; }
    
    public ETag ETag { get; set; }
}
