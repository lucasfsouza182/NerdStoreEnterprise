using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
            var loginContent = new StringContent(
                JsonSerializer.Serialize(loginUser),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("https://localhost:5001/api/identity/login", loginContent);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (!HandleResponseErrors(response))
            {
                return new UserResponse
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return JsonSerializer.Deserialize<UserResponse>(await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<UserResponse> Signin(RegisterUserViewModel registerUserViewModel)
        {
            var registerUserContent = new StringContent(
                JsonSerializer.Serialize(registerUserViewModel),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("https://localhost:5001/api/identity/signin", registerUserContent);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (!HandleResponseErrors(response))
            {
                return new UserResponse
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return JsonSerializer.Deserialize<UserResponse>(await response.Content.ReadAsStringAsync(), options);
        }
    }
}
