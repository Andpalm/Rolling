using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Carpooling.Models.Data
{
    public class Drives
    {
        [Key]
        public int ID { get; set; }
        
        public string StartingPoint { get; set; }
        
        public string Destination { get; set; }
        
        public DateTime Date { get; set; }
        
        public bool Driver { get; set; }

        public int Passengers { get; set; }
    }
}
