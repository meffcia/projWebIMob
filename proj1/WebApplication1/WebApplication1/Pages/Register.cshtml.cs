using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Imię jest wymagane.")]
        public string? FirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        public string? LastName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
        [EmailAddress(ErrorMessage = "Niepoprawny format adresu e-mail.")]
        public string? Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Hasło musi mieć co najmniej 6 znaków.")]
        public string? Password { get; set; }
        
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("/Success");
        }
    }
}
