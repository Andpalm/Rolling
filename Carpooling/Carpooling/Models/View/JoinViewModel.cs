using Carpooling.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpooling.Models.View
{
    public class JoinViewModel
    {
        public int ID { get; set; }
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        internal static void AddPerson(TrinityContext context, JoinViewModel person)
        {
            context.Persons.Add(new Persons() { FirstName = person.FirstName, LastName = person.LastName, SSN = person.SSN });
            context.SaveChanges();
        }

        internal static bool IsNotAMember(TrinityContext context, JoinViewModel person)
        {
            var personSSN = context.Persons
                .Where(p => p.SSN == person.SSN)
                .FirstOrDefault();

            if(personSSN == null)
            {
                AddPerson(context, person);
                return true;
            }
            else
                return false;
        }
    }
}
