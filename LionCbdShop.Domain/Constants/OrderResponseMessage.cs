namespace LionCbdShop.Domain.Constants;

public static class OrderResponseMessage
{
    public static string NotFound(string orderNumber) => $"Order with order number {orderNumber} not found";
}
