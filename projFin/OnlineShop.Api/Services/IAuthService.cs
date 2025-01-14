using OnlineShop.Shared;
using OnlineShop.Shared.Auth;

namespace OnlineShop.Api.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> LoginAsync(string email, string password);

        Task<ServiceResponse<int>> RegisterAsync(User user, string password);

        Task<ServiceResponse<bool>> ChangePasswordAsync(int userId, string newPassword);

        Task<bool> UserExists(string email);
        Task<ServiceResponse<List<User>>> GetUsersAsync();
    }
}
