using Carpooling.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Carpooling.Models.View
{
    public class AddDriveViewModel
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Startpunkt")]
        [Required(ErrorMessage = "Du måste ange en startpunkt")]
        public string StartingPoint { get; set; }

        [Required(ErrorMessage = "Du måste ange en slutdestination")]
        [Display(Name = "Slutdestination")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Du måste ange vilket datum samåkningen sker")]
        [Display(Name = "Datum för resa")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Du måste ange om du är förare för samåkningen")]
        [Display(Name = "Är du förare")]
        public bool Driver { get; set; }

        [Required(ErrorMessage = "Du måste ange hur många lediga platser som finns i bilen")]
        [Display(Name = "Antal lediga platser i bilen")]
        public int Passengers { get; set; }

        internal static void AddDrive(TrinityContext context, AddDriveViewModel drive)
        {
            context.Drives.Add(new Drives()
            { StartingPoint = drive.StartingPoint, Destination = drive.Destination, Date = drive.Date, Driver = drive.Driver, Passengers=drive.Passengers });
            context.SaveChanges();
        }
    }
}
