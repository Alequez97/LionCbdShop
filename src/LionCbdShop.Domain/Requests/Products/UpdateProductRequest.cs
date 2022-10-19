using LionCbdShop.Domain.Attributes;
using Microsoft.AspNetCore.Http;

namespace LionCbdShop.Domain.Requests.Products;

public class UpdateProductRequest
{
    public Guid Id { get; set; }

    public string ProductName { get; set; }

    public double OriginalPrice { get; set; }

    public double? PriceWithDiscount { get; set; }

    [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif" })]
    public IFormFile? ProductImage { get; set; }
}