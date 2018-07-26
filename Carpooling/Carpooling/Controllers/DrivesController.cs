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
            List<ShowDrivesViewModel> drives = ShowDrivesViewModel.GetDrivesListFromDataBase(context);
            return View(drives);
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
            try
            {
                if (ModelState.IsValid)
                {

                    if (JoinViewModel.IsNotAMember(context, person))
                    {
                        return RedirectToAction("Join", new { id = 2 });
                    }
                    else
                        return RedirectToAction("Join", new { id = 1 });
                }
                return Join(0);
            }
            catch
            {
                return Join(0);
            }
        }
        [HttpGet]
        public IActionResult AddDrive()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddDrive(AddDriveViewModel drive)
        {
            bool correctModel = TryValidateModel(drive);
            if (correctModel)
            {
                bool SSNExists = AddDriveViewModel.SSNInDB(context, drive);
                if (SSNExists)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["Message"] = "Ditt personnummer finns inte har du registrerat dig?";
                    return View();
                }
            }
            else
            {
                ViewData["Message"] = "Alla textrutor var inte korrekt ifyllda";
                return View();
            }
        }  
    }
}