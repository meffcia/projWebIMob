using Microsoft.AspNetCore.Mvc;

namespace proj2.Controllers
{
    public class QuotesController : Controller
    {
        // GET: /Quotes
        public IActionResult Index()
        {
            return View();
        }
    }
}
