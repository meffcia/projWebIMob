
using Microsoft.AspNetCore.Mvc;
using proj2.Models;

namespace proj2.Controllers
{
    public class CalculatorController : Controller
    {
        // GET: Calculator
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: Calculator
        [HttpPost]
        public IActionResult Index(CalculatorModel model)
        {
            if (ModelState.IsValid)
            {
                model.Result = model.Number1 + model.Number2;
            }
            return View(model);
        }
    }
}

