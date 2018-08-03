using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Carpooling.Models;

namespace Carpooling.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Contact()
        {
            ViewData["Message"] = "Kontakta oss här";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
