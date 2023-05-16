using HiCraftApi.Models;

namespace HiCraftApi.Services.Auth
{
    public interface IAuth
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> LoginAsync(LoginModel model);
        Task<AuthModel> ForgetPasswordAsync(ForgetPasswordModel model);
        Task<AuthModel> LogOutAsync();

    }
}
