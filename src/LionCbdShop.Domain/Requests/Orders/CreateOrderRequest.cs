namespace LionCbdShop.Domain.Requests.Orders;

public class CreateOrderRequest
{
    public string CustomerUsername { get; set; }

    public List<CartItem> CartItems { get; set; }
}

public class CartItem
{
    public string ProductName { get; set; }

    public int Quantity { get; set; }
}
