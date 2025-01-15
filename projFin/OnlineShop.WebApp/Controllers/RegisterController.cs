using Microsoft.AspNetCore.Mvc;
using OnlineShop.Shared.Auth;
using System.Security.Cryptography;
using System.Text;

namespace OnlineShop.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly List<User> _users; // Przyk³adowa baza danych u¿ytkowników

        public AccountController()
        {
            _users = new List<User>();
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View(new UserRegisterDto());
        }

        [HttpPost("Register")]
        public IActionResult Register(UserRegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Sprawdzenie czy email ju¿ istnieje
            if (_users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email ju¿ istnieje w systemie.");
                return View(model);
            }

            // Hashowanie has³a
            CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

            // Tworzenie nowego u¿ytkownika
            var newUser = new User
            {
                Id = _users.Count + 1,
                Username = model.UserName,
                Email = model.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = "Customer"
            };

            _users.Add(newUser);

            return RedirectToAction("Login", "Account");
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
