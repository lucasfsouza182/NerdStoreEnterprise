using System.Net.Http;
using System.Threading.Tasks;
using NSE.WebAPP.MVC.Models;

namespace NSE.WebAPP.MVC.Services
{
    public class AuthenticationService : Service, IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserResponse> Login(LoginUserViewModel loginUser)
        {
            var loginContent = GetContent(loginUser);

            var response = await _httpClient.PostAsync("https://localhost:5001/api/identity/login", loginContent);

            if (!HandleResponseErrors(response))
            {
                return new UserResponse
                {
                    ResponseResult = await DeserializeResponseOnject<ResponseResult>(response)
                };
            }

            return await DeserializeResponseOnject<UserResponse>(response);
        }

        public async Task<UserResponse> Signin(RegisterUserViewModel registerUserViewModel)
        {
            var registerUserContent = GetContent(registerUserViewModel);

            var response = await _httpClient.PostAsync("https://localhost:5001/api/identity/signin", registerUserContent);

            if (!HandleResponseErrors(response))
            {
                return new UserResponse
                {
                    ResponseResult = await DeserializeResponseOnject<ResponseResult>(response)
                };
            }

            return await DeserializeResponseOnject<UserResponse>(response);
        }
    }
}
