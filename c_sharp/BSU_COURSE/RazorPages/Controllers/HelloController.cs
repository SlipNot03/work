using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPages.Controllers
{
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome(string name, int age)
        {
            ViewData["Message"] = $"Hello {name}";
            ViewData["Age"] = age;
            ViewData["NTimes"] = age+1;
            return View();
        }
    }
}
