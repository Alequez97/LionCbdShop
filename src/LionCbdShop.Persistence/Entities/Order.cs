using System.ComponentModel.DataAnnotations;

namespace LionCbdShop.Persistence.Entities;

public class Order : EntityBase
{
    public string OrderNumber { get; set; }

    [Required]
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }

    public IList<CartItem> CartItems { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? PaymentDate { get; set; }

    public OrderStatus Status { get; set; }

    public string? CountryIso2Code { get; set; }

    public string? City { get; set; }

    public string? StreetLine1 { get; set; }

    public string? StreetLine2 { get; set; }

    public string? PostCode { get; set; }
}
