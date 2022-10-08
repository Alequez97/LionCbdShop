namespace LionCbdShop.Persistence.Entities;

public class Product : EntityBase
{
    public string Name { get; set; }

    public double OriginalPrice { get; set; }

    public double? PriceWithDiscount { get; set; }

    public string ImageName { get; set; }
}
