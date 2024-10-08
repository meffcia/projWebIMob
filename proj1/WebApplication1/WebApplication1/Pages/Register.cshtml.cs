using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Imiê jest wymagane.")]
        public string FirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        public string LastName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
        [EmailAddress(ErrorMessage = "Nieprawid³owy format adresu e-mail.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Has³o jest wymagane.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Has³o musi mieæ co najmniej 6 znaków.")]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Logika rejestracji u¿ytkownika (np. zapis do bazy danych) mo¿e byæ dodana tutaj

            return RedirectToPage("/Success"); // Przekierowanie po udanej rejestracji
        }
    }
}
