using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NSE.Core.Communication;
using NSE.WebAPP.MVC.Extensions;
using NSE.WebAPP.MVC.Models;

namespace NSE.WebAPP.MVC.Services
{
    public class AuthenticationService : Service, IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient,
                                        IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AuthenticationUrl);
            _httpClient = httpClient;
        }

        public async Task<UserResponse> Login(LoginUserViewModel loginUser)
        {
            var loginContent = GetContent(loginUser);

            var response = await _httpClient.PostAsync("/api/identity/login", loginContent);

            if (!HandleResponseErrors(response))
            {
                return new UserResponse
                {
                    ResponseResult = await DeserializeResponseObject<ResponseResult>(response)
                };
            }

            return await DeserializeResponseObject<UserResponse>(response);
        }

        public async Task<UserResponse> Signin(RegisterUserViewModel registerUserViewModel)
        {
            var registerUserContent = GetContent(registerUserViewModel);

            var response = await _httpClient.PostAsync("/api/identity/signin", registerUserContent);

            if (!HandleResponseErrors(response))
            {
                return new UserResponse
                {
                    ResponseResult = await DeserializeResponseObject<ResponseResult>(response)
                };
            }

            return await DeserializeResponseObject<UserResponse>(response);
        }
    }
}
