using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class FormModel : PageModel
    {
        private readonly ILogger<KotkiModel> _logger;

        public FormModel(ILogger<KotkiModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public string? Name { get; set; }

        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        public string? Message { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Przetwarzanie danych
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Można tutaj dodać logikę np. zapis danych lub wysłanie wiadomości
            return RedirectToPage("Success"); // Przekierowanie na stronę sukcesu
        }
    }

}
