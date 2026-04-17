using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebUI.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {

        [Route("[action]")]
        public IActionResult Login()
        {


            return View();
        }
        [Route("[action]")]
        public IActionResult Register()
        {
            return View();
        }

    }
}