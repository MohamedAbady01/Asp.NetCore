using HiCraftApi.Models;

namespace HiCraftApi.Services
{
    public interface IAuth
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> LoginAsync(LoginModel model);
        Task<AuthModel> ForgetPasswordAsync(ForgetPasswordModel model);
    }
}
