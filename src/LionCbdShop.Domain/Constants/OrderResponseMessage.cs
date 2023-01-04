namespace LionCbdShop.Domain.Constants;

public static class OrderResponseMessage
{
    public static string NotFound(string orderNumber) => $"Order with order number {orderNumber} not found";

    public static string StatusWasUpdated(string orderNumber) => $"Status was successfully updated for order with number {orderNumber}";

    public static string ShippingAddressWasUpdated(string orderNumber) => $"Shipping address was successfully updated for order with numebr {orderNumber}";
}
