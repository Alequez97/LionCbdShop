namespace LionCbdShop.Domain.Dto;

public class OrderDto
{
    public string OrderNumber { get; set; }

    public string Status { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime PaymentDate { get; set; }

    public List<CartItemDto> CartItems { get; set; }
}

public class CartItemDto
{
    public ProductDto Product { get; set; }

    public int Quantity { get; set; }
}
