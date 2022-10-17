using System.ComponentModel.DataAnnotations;

namespace LionCbdShop.Persistence.Entities;

public class CartItem : EntityBase
{
    [Required]
    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public string ProductNameDuringOrderCreation { get; set; }

    [Required]
    public int Quantity { get; set; }
}