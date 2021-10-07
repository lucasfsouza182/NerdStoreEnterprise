using System.Threading.Tasks;
using NSE.WebAPP.MVC.Models;

namespace NSE.WebAPP.MVC.Services
{
    public interface IAuthenticationService
    {
        Task<UserResponse> Login(LoginUserViewModel loginUser);

        Task<UserResponse> Signin(RegisterUserViewModel registerUserViewModel);
    }
}
