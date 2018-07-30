using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carpooling.Models.Data;

namespace Carpooling.Models.View
{
    public class MyDrivesViewModel
    {
        public string SSN { get; set; }
        public int ID { get; set; }
        public string StartingPoint { get; set; }
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public bool Driver { get; set; }
        public int Passengers { get; set; }

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

                    List<MyDrivesViewModel> drives = new List<MyDrivesViewModel>();
                    foreach (var d in myDrives)
                    {
                        drives.Add(new MyDrivesViewModel() { StartingPoint = d.StartingPoint, Destination = d.Destination, Date = d.Date, Passengers = d.Passengers, ID = d.ID });
                    }
                    return drives;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}
