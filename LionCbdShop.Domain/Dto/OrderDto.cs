namespace LionCbdShop.Domain.Dto;

public class OrderDto
{
    public List<CartItemDto> CartItems { get; set; }
}

public class CartItemDto
{
    public string ProductId { get; set; }

    public int Quantity { get; set; }
}
