using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class AjaxPageModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Debugging: Sprawdzenie, czy dane s¹ dostêpne
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email))
            {
                return BadRequest("Dane nie mog¹ byæ puste.");
            }

            // Logika przetwarzania danych
            // Mo¿esz tutaj dodaæ zapis do bazy danych lub inne operacje

            // Wys³anie odpowiedzi
            return new JsonResult($"Dane odebrane poprawnie! Imiê: {Name}, Email: {Email}");
        }
    }
}
