namespace LionCbdShop.Domain.Dto;

public class ProductDto
{
    public string Id { get; set; }

    public string ProductName { get; set; }

    public string ProductNameDuringOrderCreation { get; set; }

    public double OriginalPrice { get; set; }

    public double PriceWithDiscount { get; set; }

    public string Image { get; set; }
}