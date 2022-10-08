namespace LionCbdShop.Persistence.Entities;

public class Order : EntityBase
{
    public string OrderNumber { get; set; }

    public Customer Customer { get; set; }

    public IList<CartItem> CartItems { get; set; }
}
