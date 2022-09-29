using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CbdLion.AdminPanel.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public string Country { get; set; } = "Latvia";

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
