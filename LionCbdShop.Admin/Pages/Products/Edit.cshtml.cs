﻿using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Interfaces;
using LionCbdShop.Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CbdLion.AdminPanel.Pages.Products;

public class EditModel : PageModel
{
    private readonly IProductService _productService;
    public ProductDto Product { get; set; }
    
    public string ResponseMessage { get; set; }
    
    public bool ProductWasFound { get; set; } 

    public EditModel(IProductService productService)
    {
        _productService = productService;
    }
    
    public async Task OnGet(string id)
    {
        var response = await _productService.GetAsync(id);

        if (response.IsSuccess)
        {
            Product = response.ResponseObject;
            ProductWasFound = true;
            return;
        }

        ResponseMessage = response.Message;
    }

    public async Task<IActionResult> OnPost(UpdateProductRequest request)
    {
        var response = await _productService.UpdateAsync(request);

        return RedirectToPage("Index", response);
    }
}