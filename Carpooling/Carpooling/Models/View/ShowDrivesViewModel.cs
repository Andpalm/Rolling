using Carpooling.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpooling.Models.View
{
    public class ShowDrivesViewModel
    {
        public int ID { get; set; }
        public string StartingPoint { get; set; }
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public bool Driver { get; set; }
        public int Passengers { get; set; }

        internal static List<ShowDrivesViewModel> GetDrivesListFromDataBase(TrinityContext context)
        {
            var dataDrives = context.Drives.ToList();
            if (dataDrives != null)
            {
                List<ShowDrivesViewModel> drivesList = new List<ShowDrivesViewModel>();
                foreach (var d in dataDrives)
                {
                    drivesList.Add(new ShowDrivesViewModel()
                    { ID = d.ID, StartingPoint = d.StartingPoint, Destination = d.Destination, Date = d.Date, Driver=d.Driver, Passengers=d.Passengers });
                }
                return drivesList;
            }
            else
                return null;
        }
    }
}
