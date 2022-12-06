using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace BootVerhuurWpf
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public int MateriaalcommersarisID { get; set; }
        public Boat Boat { get; set; }

        public Reservation(DateTime date, string starttime, Boat boat) 
        {
            Date= date;
            StartTime= starttime;
            Boat= boat;
            CreatedAt = DateTime.Now.Date;

            //set in database
        }
    }
}
