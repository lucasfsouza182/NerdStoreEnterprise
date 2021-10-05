using System.Threading.Tasks;
using NSE.WebAPP.MVC.Models;

namespace NSE.WebAPP.MVC.Services
{
    public interface IAuthenticationService
    {
        Task<string> Login(LoginUserViewModel loginUser);

        Task<string> Signin(RegisterUserViewModel registerUserViewModel);
    }
}
