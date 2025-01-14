using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Api.Services;
using OnlineShop.Shared.Auth;
using OnlineShop.Shared;
using System.Security.Claims;

namespace OnlineShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // [HttpPost("Register")]
        // public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto userRegisterDto)
        // {

        //     var user = new User
        //     {
        //         Email = userRegisterDto.Email,
        //         Username = userRegisterDto.UserName
        //     };
               

        //     var response = await _authService.RegisterAsync(user, userRegisterDto.Password);

        //     if (!response.Success)
        //     {
        //         return BadRequest(response);
        //     }

        //     return Ok(response);
        // }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
        {
            var response = await _authService.LoginAsync(request.Email, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegister)
        {
            var user = new User
            {
                Email = userRegister.Email,
                Username = userRegister.UserName,
            };

            var response = await _authService.RegisterAsync(user, userRegister.Password);

            return Ok(response);
        }
        
        [HttpGet("Users")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetUsers()
        {
            var response = await _authService.GetUsersAsync();

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
