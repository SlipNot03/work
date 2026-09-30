using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebExample.Controllers
{
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome(string name, string age)
        {
            ViewData["Message"] = $"Hello {name}";
            ViewData["Age"] = age;
            ViewData["NTimes"] = Convert.ToInt32(age) + 1;
            return View();
        }
    }
}
