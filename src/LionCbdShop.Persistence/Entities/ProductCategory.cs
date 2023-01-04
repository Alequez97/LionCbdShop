using System.ComponentModel.DataAnnotations;

namespace LionCbdShop.Persistence.Entities;

public class ProductCategory : EntityBase
{
    [MaxLength(100)]
    public string Name { get; set; }
}
