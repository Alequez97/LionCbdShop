using LionCbdShop.Domain.Interfaces;
using LionCbdShop.Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LionCbdShop.Admin.Pages.Products;

public class AddModel : PageModel
{
    private readonly IProductService _productService;

    public AddModel(IProductService productService)
    {
        _productService = productService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(CreateProductRequest request)
    {
        var response = await _productService.CreateAsync(request);
        
        return RedirectToPage("Index", response);
    }
}