using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootVerhuurWpf
{
    public class Reservation
    {
        public int BoatId { get; set; }
        public string ReservationDate { get; set; }
        public string ReservationFrom { get; set; }
        public string ReservationUntil { get; set; }
        public DateTime CreatedAt { get; set; }

        public string Status { get; set; }

        public int ReservationID { get; set; }

    }
}
