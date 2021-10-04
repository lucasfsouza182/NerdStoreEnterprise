using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSE.WebAPP.MVC.Models;

namespace NSE.WebAPP.MVC.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        [Route("signin")]
        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return View(registerUser);

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
        public IActionResult Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return View(loginUser);

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
