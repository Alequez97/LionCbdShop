namespace LionCbdShop.Domain.Requests.Orders;

public class UpdateShippingAddressRequest
{
    public string OrderNumber { get; set; }

    public string CountryIso2Code { get; set; }

    public string City { get; set; }

    public string StreetLine1 { get; set; }

    public string StreetLine2 { get; set; }

    public string PostCode { get; set; }
}
