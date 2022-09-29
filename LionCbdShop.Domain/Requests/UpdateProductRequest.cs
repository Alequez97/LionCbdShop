﻿using Microsoft.AspNetCore.Http;

namespace LionCbdShop.Domain.Requests;

public class UpdateProductRequest
{
    public string Id { get; set; }

    public string ProductName { get; set; }

    public double OriginalPrice { get; set; }

    public double PriceWithDiscount { get; set; }

    public IFormFile ProductImage { get; set; }
}