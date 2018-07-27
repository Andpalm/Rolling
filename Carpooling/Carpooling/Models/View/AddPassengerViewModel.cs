using Carpooling.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Carpooling.Models.View
{
    public class AddPassengerViewModel
    {
        [Key]
        public int ID { get; set; }
        public string StartingPoint { get; set; }
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public bool Driver { get; set; }
        [Range(0, 50, ErrorMessage = "Du måste ange hur många lediga platser som finns i bilen")]
        public int Passengers { get; set; }

        [Display(Name = "Personnummer(ååååmmdd-xxxx): ")]
        [Required(ErrorMessage = "Du måste ange ett Personnummer")]
        [StringLength(13, ErrorMessage = "Du måste ange ett personnummer med 13 tecken", MinimumLength = 13)]
        public string SSN { get; set; }

        internal static AddPassengerViewModel FindDrive(TrinityContext context, int id)
        {
            var d = context.Drives.Where(x => x.ID == id).FirstOrDefault();
            AddPassengerViewModel drive = new AddPassengerViewModel()
            { ID = d.ID, Date = d.Date, StartingPoint = d.StartingPoint, Destination = d.Destination, Driver = d.Driver, Passengers = d.Passengers };
            return drive;
        }
        internal static bool SSNInDB(TrinityContext context, string ssn)
        {
            var passengerSSN = context.Persons.Where(p => p.SSN == ssn).FirstOrDefault();

            if (passengerSSN != null)
            {
                return true;
            }
            else
                return false;
        }
        internal static AddPassengerViewModel AddConnectionInPTD(TrinityContext context, AddPassengerViewModel drive, string ssn)
        {
            var person = context.Persons
                .Where(p => p.SSN == ssn)
                .FirstOrDefault();

            context.PTD.Add(new PTD() { PID = person.ID, DID = drive.ID });
            context.SaveChanges();

            var update = context.Drives.
                Where(s => s.ID == drive.ID).First();
            update.Passengers--;
            context.SaveChanges();
            
            AddPassengerViewModel updatedDrive = new AddPassengerViewModel()
            { ID = update.ID, Date = update.Date, StartingPoint = update.StartingPoint, Destination = update.Destination, Driver = update.Driver, Passengers = update.Passengers };
            return updatedDrive;

        }
        internal static bool passengerAlreadyInRide(TrinityContext context, string ssn, AddPassengerViewModel drive)
        {
            var passenger = context.Persons.Where(p => p.SSN == ssn).FirstOrDefault();
            int pid = passenger.ID;

            var passengerNotInRide = context.PTD.Where(r => r.PID == pid && r.DID == drive.ID).FirstOrDefault();
            
            if (passengerNotInRide == null)
            {
                return true;
            }
            else
                return false;
        }
        internal static AddPassengerViewModel ReturnDrive (TrinityContext context, AddPassengerViewModel drive)
        {
            var ride = context.Drives.Where(r => r.ID == drive.ID).FirstOrDefault();
            AddPassengerViewModel selectedDrive = new AddPassengerViewModel()
            { ID = ride.ID, Date = ride.Date, StartingPoint = ride.StartingPoint, Destination = ride.Destination, Driver = ride.Driver, Passengers = ride.Passengers };
            return selectedDrive;
        }

    }
}
