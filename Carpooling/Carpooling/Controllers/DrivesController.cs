using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carpooling.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace Carpooling.Controllers
{
    public class DrivesController : Controller
    {
        private TrinityContext context;

        public DrivesController(TrinityContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Join()
        {
            return View();
        }
    }
}