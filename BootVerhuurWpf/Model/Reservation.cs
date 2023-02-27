using System;

namespace BootVerhuurWpf.Model
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
