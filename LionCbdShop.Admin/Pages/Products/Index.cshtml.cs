using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductServiceActionResponse = LionCbdShop.Domain.Response;

namespace LionCbdShop.Admin.Pages.Products;

public class IndexModel : PageModel
{
    private readonly IProductService _productService;

    public IndexModel(IProductService productService)
    {
        _productService = productService;
    }
    
    public IEnumerable<ProductDto> Products { get; set; }

    public string ResponseMessage { get; set; }
    public string ResponseMessageCssClass { get; set; } = "success";

    public async Task OnGet(ProductServiceActionResponse response)
    {
        ResponseMessage = response?.Message;
        
        var getProductsResponse = await _productService.GetAllAsync();

        if (getProductsResponse.IsSuccess)
        {
            Products = getProductsResponse.ResponseObject;
            return;
        }

        ResponseMessage = getProductsResponse.Message;
        ResponseMessageCssClass = "danger";
    }

    public async Task<IActionResult> OnPostDelete(string id)
    {
        var deleteProductResponse = await _productService.DeleteAsync(new Guid(id));
        
        return RedirectToPage("Index", deleteProductResponse);
    }
}
