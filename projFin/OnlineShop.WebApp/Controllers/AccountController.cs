using Microsoft.AspNetCore.Mvc;
using OnlineShop.Shared.Auth;

namespace OnlineShop.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private static List<User> users = new List<User>
        {
            new User
            {
                Id = 1,
                Email = "test@example.com",
                Username = "testuser",
                Role = "Customer",
                PasswordHash = new byte[] { /* hash of password */ },
                PasswordSalt = new byte[] { /* salt for password hash */ }
            }
        };

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = users.FirstOrDefault(u => u.Email == userLoginDto.Email);
                if (user != null && user.PasswordHash.SequenceEqual(GenerateHash(userLoginDto.Password))) // Compare hashed password
                {
                    // Do something after successful login (e.g., set session, redirect to another page)
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Nieprawid³owy email lub has³o");
            }
            return View(userLoginDto);
        }

        private byte[] GenerateHash(string password)
        {
            // This is where password hashing logic goes
            // For simplicity, we'll return an empty byte array here, in reality, you should hash the password.
            return new byte[] { };
        }
    }
}
