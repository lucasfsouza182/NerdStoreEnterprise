using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NSE.WebAPP.MVC.Models;

namespace NSE.WebAPP.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponseHasErrors(ResponseResult response)
        {
            if (response != null && response.Errors.Messages.Any())
            {
                return true;
            }

            return false;
        }
    }
}
