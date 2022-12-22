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

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for MemberReservations.xaml
    /// </summary>
    public partial class MemberReservations : System.Windows.Window
    {
        int memberId = Int32.Parse(Login.id);
        int boatId;
        string reservationDate;
        string reservationFrom;
        string reservationUntil;
        DateTime createdAt;
        int reservationscount;
        List<int> reservationids = new List<int>();

        public MemberReservations()
        {
            InitializeComponent();
            fillDatagrid();
        }
        /// <summary>
        /// fills the datagrid with all the reservations the member has 
        /// </summary>
        private void fillDatagrid()
        {
            List<Reservation> reservations = new List<Reservation>();
            GetCountReservations();
            GetReservationId();

            while (reservations.Count < reservationscount)
            {
                foreach (int id in reservationids)
                {
                    GetReservationInfo(id);                
                    reservations.Add(new Reservation() { BoatId = boatId, ReservationDate = reservationDate, ReservationFrom = reservationFrom, ReservationUntil = reservationUntil, CreatedAt = createdAt });                  
                }
            }
            Reservationsinfo.ItemsSource = reservations;
        }

        /// <summary>
        /// Gets how many reservations there are
        /// </summary>
        public void GetCountReservations()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "127.0.0.1";
                builder.UserID = "SA";
                builder.Password = "Havermout1325";
                builder.InitialCatalog = "Bootverhuur";
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    String sql = $"Select Count (*) from reservation where member_id = {memberId}";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reservationscount = reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        /// <summary>
        /// gets all the ids from all the reservations and puts them in a list
        /// </summary>
        public void GetReservationId()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "127.0.0.1";
                builder.UserID = "SA";
                builder.Password = "Havermout1325";
                builder.InitialCatalog = "Bootverhuur";
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    String sql = $"Select * from reservation where member_id = {memberId}";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reservationids.Add(reader.GetInt32(0));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// gets all the information from a specific reservationid
        /// </summary>
        /// <param name="reservationid"></param>
        public void GetReservationInfo(int reservationid)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "127.0.0.1";
                builder.UserID = "SA";
                builder.Password = "Havermout1325";
                builder.InitialCatalog = "Bootverhuur";
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    String sql = $"Select * from reservation where reservation_id = {reservationid}";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                 boatId = reader.GetInt32(1);
                                 reservationDate = reader.GetString(2);
                                 reservationFrom = reader.GetString(3);
                                 reservationUntil = reader.GetString(4);
                                 createdAt = reader.GetDateTime(5);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
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
    }
}
