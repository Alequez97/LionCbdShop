using System.ComponentModel.DataAnnotations;

namespace LionCbdShop.Persistence.Entities;

public class Product : EntityBase
{
    public string Name { get; set; }

    public ProductCategory? Category { get; set; }

    public double OriginalPrice { get; set; }

    public double? PriceWithDiscount { get; set; }

    [MaxLength(200)]
    public string ImageName { get; set; }
}
