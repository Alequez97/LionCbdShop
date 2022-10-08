using LionCbdShop.Persistence.Constants;
using LionCbdShop.Persistence.Entities;

namespace LionCbdShop.Domain.Requests.Customers;

public class CreateCustomerRequest
{
    public string Username { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public CustomerProvider CustomerProvider { get; set; }

    public string IdInCustomerProviderSystem { get; set; }

    public string CustomerProviderAsString
    {
        get
        {
            return CustomerProvider switch
            {
                CustomerProvider.Telegram => CustomerProviderName.Telegram,
                _ => string.Empty,
            };
        }
    }
}