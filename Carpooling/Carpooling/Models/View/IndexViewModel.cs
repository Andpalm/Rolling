using Carpooling.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Carpooling.Models.View
{
    public class IndexViewModel
    {
        public int ID { get; set; }
        public string StartingPoint { get; set; }
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public bool Driver { get; set; }
        public int Passengers { get; set; }

     
        internal static List<IndexViewModel> GetDrivesListFromDataBase(TrinityContext context)
        {
            var dataDrives = context.Drives.Where(d => d.Date >= DateTime.Now && d.Passengers > 0).ToList();
            if (dataDrives != null)
            {
                List<IndexViewModel> drivesList = new List<IndexViewModel>();
                foreach (var d in dataDrives)
                {
                    drivesList.Add(new IndexViewModel()
                    { ID = d.ID, StartingPoint = d.StartingPoint, Destination = d.Destination, Date = d.Date, Driver = d.Driver, Passengers = d.Passengers });
                }
                return drivesList;
            }
            else
                return null;
        }

        internal static List<IndexViewModel> GetSpecificDrivesFromDB(TrinityContext context, string searchStart, string searchDestination)
        {
            if (searchStart != null && searchDestination != null)
            {
                var dataDrives = context.Drives
                    .Where(d => d.Date >= DateTime.Now && d.Passengers > 0 && searchStart == d.StartingPoint && searchDestination == d.Destination).ToList();
                if (dataDrives != null)
                {
                    List<IndexViewModel> drivesList = new List<IndexViewModel>();
                    foreach (var d in dataDrives)
                    {
                        drivesList.Add(new IndexViewModel()
                        { ID = d.ID, StartingPoint = d.StartingPoint, Destination = d.Destination, Date = d.Date, Driver = d.Driver, Passengers = d.Passengers });
                    }
                    return drivesList;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}
