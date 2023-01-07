using LionCbdShop.Persistence.Entities;

namespace LionCbdShop.Domain.Dto;

public class ProductDto
{
    public string Id { get; set; }

    public string ProductName { get; set; }

    public string ProductCategoryName { get; set; }

    public double OriginalPrice { get; set; }

    public double PriceWithDiscount { get; set; }

    public string Image { get; set; }
}