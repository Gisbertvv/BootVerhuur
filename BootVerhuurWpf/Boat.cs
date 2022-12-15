using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootVerhuurWpf
{
    public class Boat
    {
        public int BoatID { get; set; }
        public int AantalPersonen { get; set; } 
        public string Niveau { get; set; }
        public bool SteeringWheel { get; set; } 
        public string Status { get; set; }  

        public Boat( int boatid, int aantalPersonen, bool steeringWheel, string nivau, string status)
        {           
            BoatID = boatid;
            AantalPersonen = aantalPersonen;
            SteeringWheel = steeringWheel;
            Niveau = nivau;
            Status = status;
        }
    }
}
