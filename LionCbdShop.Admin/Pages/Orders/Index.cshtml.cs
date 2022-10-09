using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LionCbdShop.Admin.Pages.Orders;

public class IndexModel : PageModel
{
    private readonly IOrderService _orderService;

    public IndexModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public IEnumerable<OrderDto> Orders { get; set; }

    public async Task OnGet()
    {
        var response = await _orderService.GetAllAsync();

        if (response.IsSuccess)
        {
            Orders = response.ResponseObject;
        }
    }
}