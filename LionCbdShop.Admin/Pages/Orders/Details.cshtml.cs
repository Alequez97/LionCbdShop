using LionCbdShop.Domain.Dto;
using LionCbdShop.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LionCbdShop.Admin.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly IOrderService _orderService;

        public DetailsModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public OrderDto Order { get; set; }

        public async Task OnGet(string id)
        {
            var response = await _orderService.GetAsync(new Guid(id));

            if (response.IsSuccess)
            {
                Order = response.ResponseObject;
            }
        }
    }
}
