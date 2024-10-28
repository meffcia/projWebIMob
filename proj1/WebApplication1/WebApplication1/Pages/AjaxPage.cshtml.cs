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

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            Console.Write("TUtaj");
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { error = $"Model state is not valid" });
            }

            return new JsonResult(new { message = $"Dane odebrane poprawnie!" });
        }
    }
}
