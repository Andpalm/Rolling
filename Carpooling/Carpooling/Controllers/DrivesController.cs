using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carpooling.Models.Data;
using Carpooling.Models.View;
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

        [HttpGet]
        public IActionResult Join(int id)
        {
            var person = new JoinViewModel() { ID = id };
            return View(person);
        }

        [HttpPost]
        public IActionResult Join(JoinViewModel person)
        {
            if (JoinViewModel.IsNotAMember(context, person))
            {
                return RedirectToAction("Join", new { id = 2 });
            }
            else
                return RedirectToAction("Join", new { id = 1 });
        }
    }
}