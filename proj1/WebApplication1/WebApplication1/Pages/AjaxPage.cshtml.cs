using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Pages
{
    public class AjaxPageModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Imię jest wymagane.")]
        public string? Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
        [EmailAddress(ErrorMessage = "Niepoprawny format adresu e-mail.")]
        public string? Email { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Debugging: Sprawdzenie, czy dane s� dost�pne
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email))
            {
                return BadRequest("Dane nie mog� by� puste.");
            }

            // Logika przetwarzania danych
            // Mo�esz tutaj doda� zapis do bazy danych lub inne operacje

            // Wys�anie odpowiedzi
            return new JsonResult($"Dane odebrane poprawnie! Imi�: {Name}, Email: {Email}");
        }
    }
}
