using Carpooling.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpooling.Models.View
{
    public class ShowPassengersViewModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }

        internal static List<ShowPassengersViewModel> ListPassengers(TrinityContext context, int id)
        {
            List<ShowPassengersViewModel> passenger = new List<ShowPassengersViewModel>();
            var personID = context.PTD.Where(x => x.DID == id).Select(x => x.PID);
            foreach (var i in personID)
            {
                var person = context.Persons.Where(x => x.ID == i).FirstOrDefault();
                passenger.Add(new ShowPassengersViewModel()
                { ID = person.ID, FirstName = person.FirstName, LastName = person.LastName, SSN = person.SSN });
            }
            return passenger;
        }


    }
}
