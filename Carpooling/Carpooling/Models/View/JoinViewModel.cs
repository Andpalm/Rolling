using Carpooling.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Carpooling.Models.View
{
    public class JoinViewModel
    {
        public int ID { get; set; }
        [Display(Name ="Personnummer(ååååmmdd-xxxx): ")]
        [Required(ErrorMessage = "Du måste ange ett Personnummer")]
        [StringLength(13, ErrorMessage ="Du måste ange ett personnummer med 13 tecken", MinimumLength = 13)]
        public string SSN { get; set; }
        [Display(Name = "Förnamn: ")]
        [Required(ErrorMessage ="Du måste ange ett förnamn")]
        public string FirstName { get; set; }
        [Display(Name ="Efternamn: ")]
        [Required(ErrorMessage ="Du måste Ange ett efternamn")]
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
