using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_Shopping.WebUI.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [Route("/Admin/[controller]/List")]
        [HttpGet]
        public IActionResult CAIndex()
        {
            return View();
        }

        [Route("/Admin/[controller]/Create")]
        [HttpGet]
        public IActionResult CACreate()
        {
            return View();
        }
        [Route("/Admin/[controller]/Update")]
        [HttpGet]
        public IActionResult CAUpdate()
        {
            return View();
        }
    }
}