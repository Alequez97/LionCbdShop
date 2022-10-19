using LionCbdShop.Persistence.Entities;

namespace LionCbdShop.Domain.Dto;

public class OrderDto
{
    public string Id { get; set; }

    public string OrderNumber { get; set; }

    public string CustomerUsername { get; set; }

    public string Status { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? PaymentDate { get; set; }

    public List<CartItemDto> CartItems { get; set; }

    public bool HasBeenPaid => Status == OrderStatus.Paid.ToString();
}

public class CartItemDto
{
    public ProductDto Product { get; set; }
    
    public string ProductNameDuringOrderCreation { get; set; }

    public int Quantity { get; set; }
}
