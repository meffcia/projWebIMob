using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Imi� jest wymagane.")]
        public string FirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        public string LastName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
        [EmailAddress(ErrorMessage = "Nieprawid�owy format adresu e-mail.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Has�o jest wymagane.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Has�o musi mie� co najmniej 6 znak�w.")]
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

            // Logika rejestracji u�ytkownika (np. zapis do bazy danych) mo�e by� dodana tutaj

            return RedirectToPage("/Success"); // Przekierowanie po udanej rejestracji
        }
    }
}
