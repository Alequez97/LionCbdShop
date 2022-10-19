using Microsoft.AspNetCore.Http;

namespace LionCbdShop.Domain.Requests.Products;

public class CreateProductRequest
{
    public string ProductName { get; set; }

    public double OriginalPrice { get; set; }

    public double? PriceWithDiscount { get; set; }

    public IFormFile ProductImage { get; set; }
}
