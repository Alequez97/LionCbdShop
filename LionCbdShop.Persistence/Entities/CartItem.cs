namespace LionCbdShop.Persistence.Entities;

public class CartItem : EntityBase
{
    public Product Product { get; set; }

    public int Quantity { get; set; }
}