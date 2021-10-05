using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NSE.WebAPP.MVC.Models;

namespace NSE.WebAPP.MVC.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Login(LoginUserViewModel loginUser)
        {
            var loginContent = new StringContent(
                JsonSerializer.Serialize(loginUser),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("/api/identity/login", loginContent);

            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Signin(RegisterUserViewModel registerUserViewModel)
        {
            var registerUserContent = new StringContent(
                JsonSerializer.Serialize(registerUserViewModel),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("https://localhost:5001/api/identity/signin", registerUserContent);

            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
        }
    }
}
