using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Pages
{
    public class FormModel : PageModel
    {
        private readonly ILogger<FormModel> _logger;

        public FormModel(ILogger<FormModel> logger)
        {
            _logger = logger;
        }

        // Właściwości powiązane z formularzem
        [BindProperty]
        [Required(ErrorMessage = "Imię jest wymagane.")]
        public string Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Adres email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres email.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Wiadomość jest wymagana.")]
        public string Message { get; set; }

        // Akcja dla żądań GET
        public void OnGet()
        {
        }

        // Akcja dla żądań POST
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) // Sprawdzenie walidacji modelu
            {
                return Page(); // Zwrócenie formularza z błędami
            }

            // Po pomyślnym przesłaniu formularza nie musisz nic robić tutaj,
            // ponieważ dane będą wyświetlane poniżej formularza w widoku.
            return Page(); // Pozwól na pozostanie na tej samej stronie
        }
    }

}
