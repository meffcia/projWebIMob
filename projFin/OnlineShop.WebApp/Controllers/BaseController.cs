using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace OnlineShop.WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected int GetUserIdFromSession()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                throw new Exception("Nie znaleziono tokena w sesji. Zaloguj się ponownie.");

            return GetUserIdFromToken(token);
        }

        private int GetUserIdFromToken(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            if (!jwtHandler.CanReadToken(token))
                throw new Exception("Nieprawidłowy token JWT.");

            var jwtToken = jwtHandler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userIdClaim == null)
                throw new Exception("Nie znaleziono identyfikatora użytkownika w tokenie JWT.");

            return int.Parse(userIdClaim.Value);
        }
    }
}
