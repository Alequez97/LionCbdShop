using System.ComponentModel.DataAnnotations;

namespace LionCbdShop.Persistence.Entities;

public class Customer : EntityBase
{
    [Required]
    public string Username { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public CustomerProvider CustomerProvider { get; set; }

    public string IdInCustomerProviderSystem { get; set; }
}
