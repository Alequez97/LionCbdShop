namespace LionCbdShop.Domain.Requests.Orders;

public class CreateOrderRequest
{
    public string IdInCustomerProviderSystem { get; set; }
    
    public string CustomerUsername { get; set; }

    public List<CreateOrderRequestCartItem> CartItems { get; set; }
}

public class CreateOrderRequestCartItem
{
    public string ProductId { get; set; }
    
    public string ProductNameDuringOrderCreation { get; set; }

    public int Quantity { get; set; }
}
