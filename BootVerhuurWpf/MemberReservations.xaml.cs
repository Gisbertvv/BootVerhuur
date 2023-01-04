using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Windows.ApplicationModel.Activation;
using Syncfusion.Windows.Shared;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for MemberReservations.xaml
    /// </summary>
    public partial class MemberReservations : System.Windows.Window
    {
        int boatId;
        string reservationDate;
        string reservationFrom;
        string reservationUntil;
        DateTime createdAt;
        int reservationscount;
        string status;
        string date1;
        string date2;
        List<int> reservationids;
        List<int> reservationids2;
        MemberReservationsSql MemberReservationsSql = new MemberReservationsSql();

        public MemberReservations()
        {
            InitializeComponent();
            GetReservationDates();
            MemberReservationsSql.ChangeStatus(date1,date2);
            fillDatagrid();
            fillDatagrid2();
        }

        /// <summary>
        /// Gets the availeble reservationdates
        /// </summary>
        private void GetReservationDates()
        {
            DateTime plusone = DateTime.Now.AddDays(1);
            DateTime plustwo = DateTime.Now.AddDays(2);

            if (plusone.DayOfWeek == DayOfWeek.Saturday)
            {
                date1 = DateTime.Now.AddDays(3).ToShortDateString();
                date2 = DateTime.Now.AddDays(4).ToShortDateString();
            }
            else if (plustwo.DayOfWeek == DayOfWeek.Saturday)
            {
                date1 = DateTime.Now.AddDays(1).ToShortDateString();
                date2 = DateTime.Now.AddDays(4).ToShortDateString();
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                date1 = DateTime.Now.AddDays(2).ToShortDateString();
                date2 = DateTime.Now.AddDays(3).ToShortDateString();
            }
            else if (plustwo.DayOfWeek == DayOfWeek.Sunday)
            {
                date1 = DateTime.Now.AddDays(3).ToShortDateString();
                date2 = DateTime.Now.AddDays(4).ToShortDateString();
            }
            else if (plusone.DayOfWeek == DayOfWeek.Sunday)
            {
                date1 = DateTime.Now.AddDays(2).ToShortDateString();
                date2 = DateTime.Now.AddDays(3).ToShortDateString();
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                date1 = DateTime.Now.AddDays(1).ToShortDateString();
                date2 = DateTime.Now.AddDays(2).ToShortDateString();
            }
            else
            {
                date1 = DateTime.Now.AddDays(1).ToShortDateString();
                date2 = DateTime.Now.AddDays(2).ToShortDateString();
            }
        }
        /// <summary>
        /// fills the datagrid with all the reservations the member has 
        /// </summary>
        private void fillDatagrid()
        {
            
            List<Reservation> reservations = new List<Reservation>();
            MemberReservationsSql.GetCountReservations();
            reservationscount = MemberReservationsSql.reservationscount;
            MemberReservationsSql.GetReservationId();
            reservationids = MemberReservationsSql.reservationids;

            while (reservations.Count < reservationscount)
            {
                foreach (int id in reservationids)
                {
                    MemberReservationsSql.GetReservationInfo(id);
                     boatId = MemberReservationsSql.boatId;
                     reservationDate =MemberReservationsSql.reservationDate;
                     reservationFrom = MemberReservationsSql.reservationFrom ;
                     reservationUntil = MemberReservationsSql.reservationUntil;
                     createdAt = MemberReservationsSql.createdAt;
                     status = MemberReservationsSql.status;
                    reservations.Add(new Reservation() { ReservationID=id,BoatId = boatId, ReservationDate = reservationDate, ReservationFrom = reservationFrom, ReservationUntil = reservationUntil, CreatedAt = createdAt, Status = status });                  
                }
            }
            Reservationsinfo.ItemsSource = reservations;
        }
        /// <summary>
        /// fills the datagrid with all the active reservations the member has
        /// </summary>
        private void fillDatagrid2()
        {

            List<Reservation> reservations = new List<Reservation>();
            MemberReservationsSql.GetCountActiveReservations();
            reservationscount = MemberReservationsSql.reservationscount;
            MemberReservationsSql.GetActiveReservationId();
            reservationids = MemberReservationsSql.reservationids;

            while (reservations.Count < reservationscount)
            {
                foreach (int id in reservationids)
                {
                    MemberReservationsSql.GetReservationInfo(id);
                    boatId = MemberReservationsSql.boatId;
                    reservationDate = MemberReservationsSql.reservationDate;
                    reservationFrom = MemberReservationsSql.reservationFrom;
                    reservationUntil = MemberReservationsSql.reservationUntil;
                    createdAt = MemberReservationsSql.createdAt;
                    status = MemberReservationsSql.status;
                    reservations.Add(new Reservation() { ReservationID = id, BoatId = boatId, ReservationDate = reservationDate, ReservationFrom = reservationFrom, ReservationUntil = reservationUntil, CreatedAt = createdAt, Status = status });
                }
            }
            Activeresrevationinfo.ItemsSource = reservations;
        }

        private void Open_AdminPanel(object sender, RoutedEventArgs e)
        {
            AdminPanel adminPanel= new AdminPanel();
            adminPanel.Show();
            Close();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            Create create= new Create();
            create.Show();
            Close();
        }

        private void reserve(object sender, RoutedEventArgs e)
        {
            Temp temp= new Temp();
            temp.Show();
            Close();
        }

        private void AccidentReport(object sender, RoutedEventArgs e)
        {
            
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// When button is pressed status of reservation is changed to 'Geanulleerd'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_reservation(object sender, RoutedEventArgs e)
        {
            int i = Activeresrevationinfo.SelectedIndex;
            if (i == -1)
            {
                MessageBox.Show("Selecteer een reservering om te anulleren");
            }
            else
            {
                MemberReservationsSql.GetReservationIds();
                reservationids2 = MemberReservationsSql.reservationids2;
                int id = reservationids2[i];
                MemberReservationsSql.CancelReservation(id);
                fillDatagrid();
                fillDatagrid2();
            }
        }
    }
}
