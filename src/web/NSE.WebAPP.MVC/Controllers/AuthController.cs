using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSE.WebAPP.MVC.Models;
using NSE.WebAPP.MVC.Services;

namespace NSE.WebAPP.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        [Route("signin")]
        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SigninAsync(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return View(registerUser);

            var response = await _authenticationService.Signin(registerUser);

            if (false)
            {
                return View(registerUser);
            }


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return View(loginUser);

            var response = await _authenticationService.Login(loginUser);

            if (false)
            {
                return View(loginUser);
            }


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
