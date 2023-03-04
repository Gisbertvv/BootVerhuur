using BootVerhuurWpf;
using Syncfusion.Windows.Shared;
using Windows.Media.Audio;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void InsertReservation_Should_Return_True_When_Successful()
        {


            // Arrange
            //Get the right dates to use as input
            Booking b = new Booking();
            b.AdjustCalender();
            RentalController service = new RentalController(b.date1, b.date2);
            string reservationdate = b.date1;
            string reservationtime = "10:00";
            string endtime = "12:00";
            int count = service.GetMemberIdCountReservations(b.date1, b.date2);

            if(count == 2 || count == 1)
            {
                service.GetReservationIds();
                service.CancelReservation(service.reservationIDS2[0]);
            }
            // Act
            bool result = service.InsertReservation(reservationdate, reservationtime, endtime);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void InsertReservation_Should_Return_False_When_Member_Has_Max_Reservations()
        {

            // Arrange
            //Get the right dates to use as input
            Booking b = new Booking();
            b.AdjustCalender();
            RentalController service = new RentalController(b.date1,b.date2);
            string reservationdate = b.date2;
            string reservationtime = "10:00";
            string endtime = "14:00";
            int count = service.GetMemberIdCountReservations(b.date1, b.date2);

            // Make the member have 2 reservations already
            if(count == 1)
            {
                service.InsertReservation(reservationdate, reservationtime, endtime);
            }
            else if(count == 0)
            {
                service.InsertReservation(reservationdate, reservationtime, endtime);
                service.InsertReservation(reservationdate, reservationtime, endtime);
            }
            
            // Act
            bool result = service.InsertReservation(reservationdate, reservationtime, endtime);

            // Assert
            Assert.IsFalse(result);
        }

    }
}