using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carpooling.Models.Data;

namespace Carpooling.Models.View
{
    public class MyDrivesViewModel
    {
        public int ID { get; set; }
        public string StartingPoint { get; set; }
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public int Passengers { get; set; }
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        internal static List<MyDrivesViewModel> GetPersonsDrives(TrinityContext context, string ssn)
        {
            var pid = context.Persons
                .Where(p => p.SSN == ssn)
                .Select(p => p.ID)
                .FirstOrDefault();

            if (pid != 0)
            {
                var did = context.PTD
                    .Where(i => i.PID == pid)
                    .Select(i => i.DID)
                    .ToList();

                if (did != null)
                {
                    var myDrives = context.Drives
                        .Where(d => did.Contains(d.ID))
                        .ToList();

                    var person = context.Persons
                        .Where(p => p.SSN == ssn)
                        .FirstOrDefault();

                    List<MyDrivesViewModel> drives = new List<MyDrivesViewModel>();
                    foreach (var d in myDrives)
                    {
                        drives.Add(new MyDrivesViewModel() { StartingPoint = d.StartingPoint, Destination = d.Destination, Date = d.Date, Passengers = d.Passengers, ID = d.ID, SSN = ssn, FirstName = person.FirstName, LastName = person.LastName });
                    }
                    return drives;
                }
                else
                    return null;
            }
            else
                return null;
        }

        internal static void RemoveTraveller(MyDrivesViewModel drive, TrinityContext context)
        {
            var person = context.Persons
                .Where(p => p.SSN == drive.SSN)
                .First();

            var travellers = context.PTD
                .Where(t => t.DID == drive.ID)
                .ToList();

            if (travellers[0].PID == person.ID)
            {
                foreach (var item in travellers)
                {
                    context.Remove(item);
                    context.SaveChanges();
                }

                var _drive = context.Drives
                    .Where(d => d.ID == drive.ID)
                    .FirstOrDefault();
                context.Remove(_drive);
                context.SaveChanges();
            }
            else
            {
                var updatePassengers = context.Drives
                    .Where(p => p.ID == drive.ID)
                    .FirstOrDefault();
                updatePassengers.Passengers++;
                context.SaveChanges();

                var traveller = travellers
                    .Where(p => p.PID == person.ID)
                    .FirstOrDefault();
                context.Remove(traveller);
                context.SaveChanges();
            }
        }
    }
}
