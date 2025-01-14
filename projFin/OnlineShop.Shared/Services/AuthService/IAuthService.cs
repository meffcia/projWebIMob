using OnlineShop.Shared.Auth;

namespace OnlineShop.Shared.Services.AuthService
{
    public interface IAuthService
    {
        
        Task<ServiceResponse<string>> Login(UserLoginDto userLogin);

        Task<ServiceResponse<int>> Register(UserRegisterDto userRegister);

        Task<ServiceResponse<bool>> ChangePassword(string newPassword);
    }
}
