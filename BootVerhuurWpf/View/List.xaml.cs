
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using BootVerhuurWpf.Model;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for Temp.xaml
    /// </summary>
    public partial class List : Window
    {

        int numberOfPeople;
        string boatingLevel;
        bool steeringWheel;
        string status;
        int countBoats;
        int id;
        RentalController tempSql = new RentalController();
        public List()
        {
            InitializeComponent();
            ShowBoats();
        }
        /// <summary>
        /// Fills the datagrid with all the boats for the level of the member
        /// </summary>
        private void ShowBoats()
        {
            List<Boat> boats = new List<Boat>();
            tempSql.GetRightId();
            id = tempSql.ID;
            countBoats = tempSql.GetCountBoats();
            while (boats.Count < countBoats)
            {
                tempSql.GetBoatInfo(id);
                numberOfPeople = tempSql.NumberOfPeople;
                boatingLevel = tempSql.BoatingLevel;
                steeringWheel = tempSql.SteeringWheel;
                status = tempSql.Status;
                boats.Add(new Boat(id, numberOfPeople, steeringWheel, boatingLevel, status));
                id++;
            }
            Boats.ItemsSource = boats;
        }
        /// <summary>
        /// When boat is selected opens new window. With the information of the boat_id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedBoat(object sender, MouseButtonEventArgs e)
        {

            tempSql.GetRightId();
            id = tempSql.ID;
            int i = Boats.SelectedIndex;

            i += id;

            Booking bk = new Booking(i);
            bk.Show();
            Close();
        }

        private void MemberReservations(object sender, RoutedEventArgs e)
        {
            Reservations memberreserveration = new Reservations();
            memberreserveration.Show();
            Close();
        }
            private void AccidentReport(object sender, RoutedEventArgs e)
            {
                PDF window = new PDF();
                window.Show();
            }

            private void Logout(object sender, RoutedEventArgs e)
            {
                Login window = new Login();
                window.Show();
                Close();
            }

            private void BackClick(object sender, RoutedEventArgs e)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
        }
    }

