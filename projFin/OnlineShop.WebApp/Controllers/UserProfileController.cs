using Microsoft.AspNetCore.Mvc;
using OnlineShop.Shared.Models; // Przyk�adowy namespace, zmie� w zale�no�ci od swojego projektu

namespace OnlineShop.WebApp.Controllers
{
    public class UserProfileController : Controller
    {
        // Akcja do wy�wietlania profilu u�ytkownika
        public IActionResult Index()
        {
            // Przyk�ad pobrania danych u�ytkownika (tutaj przekazujemy mockowe dane)
            var userProfile = new UserProfile
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "jan.kowalski@example.com",
                Phone = "123-456-789"
            };

            return View(userProfile);
        }

        // Akcja do edytowania danych u�ytkownika
        [HttpGet]
        public IActionResult Edit()
        {
            // Pobieranie danych u�ytkownika do edycji
            var userProfile = new UserProfile
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "jan.kowalski@example.com",
                Phone = "123-456-789"
            };

            return View(userProfile);
        }

        // Akcja do zapisania zaktualizowanych danych u�ytkownika
        [HttpPost]
        public IActionResult Edit(UserProfile model)
        {
            if (ModelState.IsValid)
            {
                // Logika zapisu danych do bazy
                // Za��my, �e zapisali�my dane poprawnie

                // Po zapisaniu przekierowujemy u�ytkownika na stron� z jego profilem
                return RedirectToAction("Index");
            }

            // Je�li model jest nieprawid�owy, wracamy do widoku edytowania
            return View(model);
        }
    }
}
