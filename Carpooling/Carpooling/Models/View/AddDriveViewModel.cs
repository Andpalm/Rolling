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

        [Range(1, 50, ErrorMessage = "Du måste ange hur många lediga platser som finns i bilen")]
        [Display(Name = "Antal lediga platser i bilen")]
        public int Passengers { get; set; }

        [Display(Name = "Personnummer(ååååmmdd-xxxx): ")]
        [Required(ErrorMessage = "Du måste ange ett Personnummer")]
        [StringLength(13, ErrorMessage = "Du måste ange ett personnummer med 13 tecken", MinimumLength = 13)]
        public string SSN { get; set; }

        internal static int AddDrive(TrinityContext context, AddDriveViewModel drive)
        {
            Drives newDrive = new Drives()
            { StartingPoint = drive.StartingPoint, Destination = drive.Destination, Date = drive.Date, Driver = drive.Driver, Passengers = drive.Passengers };

            context.Drives.Add(newDrive);
            context.SaveChanges();

            int did = newDrive.ID;
            return (did);
        }
        internal static bool SSNInDB(TrinityContext context, AddDriveViewModel drive)
        {
            var driveSSN = context.Persons
                .Where(p => p.SSN == drive.SSN)
                .FirstOrDefault();

            if (driveSSN != null)
            {
                int did = AddDrive(context, drive);
                var person = context.Persons.Where(p => p.SSN == drive.SSN).FirstOrDefault();
             
                context.PTD.Add(new PTD() { PID = person.ID, DID = did });
                context.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
