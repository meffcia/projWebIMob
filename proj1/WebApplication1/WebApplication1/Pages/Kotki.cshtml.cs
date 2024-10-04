using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class KotkiModel : PageModel
    {
        private readonly ILogger<KotkiModel> _logger;

        public KotkiModel(ILogger<KotkiModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }

}
