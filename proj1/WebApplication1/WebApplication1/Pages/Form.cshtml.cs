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
            IsFormSubmitted = false;
        }

        [BindProperty]
        [Required(ErrorMessage = "Imię jest wymagane.")]
        public string? Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Adres email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres email.")]
        public string? Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Wiadomość jest wymagana.")]
        public string? Message { get; set; }

        public bool IsFormSubmitted { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            IsFormSubmitted = true;

            return Page();
        }

        public IActionResult OnPostConfirm()
        {
            return RedirectToPage("FormSuccess");
        }
    }
}
