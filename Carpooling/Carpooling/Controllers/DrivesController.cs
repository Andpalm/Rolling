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
            try
            {
                List<IndexViewModel> drives = IndexViewModel.GetDrivesListFromDataBase(context);
                return View(drives);
            }
            catch
            {
                ViewData["Message"] = "Det finns inga registrerade resor";
                return View();
            }
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
                        return Join(2);
                    }
                    else
                        return Join(1);
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

        [HttpGet]
        public IActionResult AddPassenger(int id)
        {
            AddPassengerViewModel drive = AddPassengerViewModel.FindDrive(context, id);
            return View(drive);
        }

        [HttpPost]
        public IActionResult AddPassenger(string ssn, AddPassengerViewModel drive)
        {
            AddPassengerViewModel currentDrive = AddPassengerViewModel.ReturnDrive(context, drive);
            bool SSNExists = AddPassengerViewModel.SSNInDB(context, ssn);
            if (SSNExists)
            {
                bool PassengerNotInRide = AddPassengerViewModel.passengerAlreadyInRide(context, ssn, drive);
                if (PassengerNotInRide)
                {
                    if (currentDrive.Passengers > 0)
                    {
                        AddPassengerViewModel updatedDrive = AddPassengerViewModel.AddConnectionInPTD(context, drive, ssn);
                        ViewData["Message"] = "Du är inbokad på resan!";
                        return View(updatedDrive);
                    }
                    else
                    {
                        ViewData["Message"] = "Bilen är redan full.";
                        return View(currentDrive);
                    }
                }
                else
                {
                    ViewData["Message"] = "Du är redan inbokad på den här resan.";
                    return View(currentDrive);
                }
            }
            else
            {
                ViewData["Message"] = "Ditt personnummer finns inte har du registrerat dig?";
                return View(currentDrive);
            }
        }

        [HttpGet]
        public IActionResult ShowPassengers(int id)
        {
            List<ShowPassengersViewModel> passengers = ShowPassengersViewModel.ListPassengers(context, id);
            return View(passengers);
        }

        [HttpGet]
        public IActionResult MyDrives(int id)
        {
            if (id == 1)
            {
                ViewData["Message"] = "Du är borttagen från turen!";
                return View();
            }
            return View();
        }

        [HttpPost]
        public IActionResult MyDrives(string ssn)
        {
            if (ssn != null)
            {
                List<MyDrivesViewModel> myDrives = MyDrivesViewModel.GetPersonsDrives(context, ssn);
                if (myDrives.Count != 0)
                    return View(myDrives);
                else
                {
                    ViewData["Message"] = "Personen med det angivna personnumret finns inte med i några turer!";
                    return View();
                }
            }
            else
                return View();
        }

        [HttpGet]
        public IActionResult RemoveTraveller(MyDrivesViewModel drive)
        {
            MyDrivesViewModel.RemoveTraveller(drive, context);
            return RedirectToAction("MyDrives", new { id = 1 });
        }
    }
}