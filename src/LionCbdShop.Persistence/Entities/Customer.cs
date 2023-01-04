using System.ComponentModel.DataAnnotations;

namespace LionCbdShop.Persistence.Entities;

public class Customer : EntityBase
{
    [Required]
    [MaxLength(100)]
    public string Username { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [MaxLength(100)]
    public string? LastName { get; set; }

    public CustomerProvider? CustomerProvider { get; set; }

    [MaxLength(100)]
    public string? IdInCustomerProviderSystem { get; set; }
}
