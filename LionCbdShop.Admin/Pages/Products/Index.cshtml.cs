using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LionCbdShop.Admin.Pages.Products;

public class IndexModel : PageModel
{
    private readonly IProductService _productService;

    public IndexModel(IProductService productService)
    {
        _productService = productService;
    }

    public GetProductsResponse GetProductsResponse { get; set; }

    public DomainLayerResponse ProductServicePostActionResponse { get; set; }

    public async Task OnGet(DomainLayerResponse? response)
    {
        ProductServicePostActionResponse = response;

        GetProductsResponse = await _productService.GetAllAsync();
    }

    public async Task<IActionResult> OnPostDelete(string id)
    {
        var deleteProductResponse = await _productService.DeleteAsync(new Guid(id));

        return RedirectToPage("Index", deleteProductResponse);
    }
}