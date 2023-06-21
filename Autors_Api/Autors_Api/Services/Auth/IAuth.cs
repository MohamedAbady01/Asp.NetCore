using Autors_Api.Models;

namespace Autors_Api.Services.Auth
{
    public interface IAuth
    {
        Task<AuthModel> LogInAsync(LoginModel model);

    }
}
