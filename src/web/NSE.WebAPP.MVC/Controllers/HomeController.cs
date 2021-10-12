using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSE.WebAPP.MVC.Models;

namespace NSE.WebAPP.MVC.Controllers
{
    public class HomeController : Controller
    {
        [Route("system-unavailable")]
        public IActionResult SistemaIndisponivel()
        {
            var modelError = new ErrorViewModel
            {
                Message = "The system is temporarily unavailable, this can occur in times of user overload.",
                Title = "System unavailable.",
                ErrorCode = 500
            };

            return View("Error", modelError);
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Message = "An error has occurred! Please try again later or contact our support.";
                modelErro.Title = "An error has occurred!";
                modelErro.ErrorCode = id;
            }
            else if (id == 404)
            {
                modelErro.Message =
                    "The page you are looking for does not exist! <br />If you have any questions, please contact our support";
                modelErro.Title = "Ops! Page not found.";
                modelErro.ErrorCode = id;
            }
            else if (id == 403)
            {
                modelErro.Message = "You are not allowed to do this.";
                modelErro.Title = "Access Denied";
                modelErro.ErrorCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", modelErro);
        }
    }
}
