namespace LionCbdShop.Domain.Requests.Orders;

public class CreateOrderRequest
{
    public List<CartItem> CartItems { get; set; }
}

public class CartItem
{
    public string ProductId { get; set; }

    public int Quantity { get; set; }
}
