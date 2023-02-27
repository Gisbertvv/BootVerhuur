using System;
using System.Collections.Generic;
using System.Windows;
using BootVerhuurWpf.Model;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for MemberReservations.xaml
    /// </summary>
    public partial class Reservations : System.Windows.Window
    {
        int boatId;
        string reservationDate;
        string reservationFrom;
        string reservationUntil;
        DateTime createdAt;
        int reservationsCount;
        string status;
        string date1;
        string date2;
        List<int> reservationIds;
        List<int> reservationIds2;
        RentalController MemberReservationsSql = new RentalController();

        public Reservations()
        {
            InitializeComponent();
            GetReservationDates();
            MemberReservationsSql.ChangeStatus(date1,date2);
            FillDatagrid();
            FillDatagrid2();
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
        private void FillDatagrid()
        {
            
            List<Reservation> reservations = new List<Reservation>();
            MemberReservationsSql.GetCountReservations();
            reservationsCount = MemberReservationsSql.reservationscount;
            MemberReservationsSql.GetReservationId();
            reservationIds = MemberReservationsSql.reservationIDS;

            while (reservations.Count < reservationsCount)
            {
                foreach (int id in reservationIds)
                {
                    MemberReservationsSql.GetReservationInfo(id);
                     boatId = MemberReservationsSql.BoatID;
                     reservationDate =MemberReservationsSql.ReservationDate;
                     reservationFrom = MemberReservationsSql.ReservationFrom ;
                     reservationUntil = MemberReservationsSql.ReservationUntil;
                     createdAt = MemberReservationsSql.CreatedAt;
                     status = MemberReservationsSql.Status;
                    reservations.Add(new Reservation() { ReservationID=id,BoatId = boatId, ReservationDate = reservationDate, ReservationFrom = reservationFrom, ReservationUntil = reservationUntil, CreatedAt = createdAt, Status = status });                  
                }
            }
            Reservationsinfo.ItemsSource = reservations;
        }
        /// <summary>
        /// fills the datagrid with all the active reservations the member has
        /// </summary>
        private void FillDatagrid2()
        {

            List<Reservation> reservations = new List<Reservation>();
            MemberReservationsSql.GetCountActiveReservations();
            reservationsCount = MemberReservationsSql.reservationscount;
            MemberReservationsSql.GetActiveReservationId();
            reservationIds = MemberReservationsSql.reservationIDS;

            while (reservations.Count < reservationsCount)
            {
                foreach (int id in reservationIds)
                {
                    MemberReservationsSql.GetReservationInfo(id);
                    boatId = MemberReservationsSql.BoatID;
                    reservationDate = MemberReservationsSql.ReservationDate;
                    reservationFrom = MemberReservationsSql.ReservationFrom;
                    reservationUntil = MemberReservationsSql.ReservationUntil;
                    createdAt = MemberReservationsSql.CreatedAt;
                    status = MemberReservationsSql.Status;
                    reservations.Add(new Reservation() { ReservationID = id, BoatId = boatId, ReservationDate = reservationDate, ReservationFrom = reservationFrom, ReservationUntil = reservationUntil, CreatedAt = createdAt, Status = status });
                }
            }
            Activeresrevationinfo.ItemsSource = reservations;
        }

        private void Open_AdminPanel(object sender, RoutedEventArgs e)
        {
            Settings adminPanel= new Settings();
            adminPanel.Show();
            Close();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            Popup create= new Popup();
            create.Show();
            Close();
        }

        private void Reserve(object sender, RoutedEventArgs e)
        {
            List temp= new List();
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
                reservationIds2 = MemberReservationsSql.reservationIDS2;
                int id = reservationIds2[i];
                MemberReservationsSql.CancelReservation(id);
                FillDatagrid();
                FillDatagrid2();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
