using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using Windows.Media.Playback;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for BookBoat.xaml
    /// </summary>
    public partial class BookBoat : Window
    {
        public int aantalp;
        public string bootniveau;
        public bool stir;
        int Id;
        int reservationId;
        string status;
        string reservationendtime;

        public BookBoat(int id)
        { 
            Id = id;
            InitializeComponent();
            AdjustCalender();
            AdjustTimeBox();
            Checkeverything(Id);          
        }
        /// <summary>
        /// maybe change to get availeble time from database, or set time each 2 hours
        /// </summary>
        private void AdjustTimeBox()
        {
            int timecount =0;
            Gekozentijd.Items.Add($"0{timecount}:00");
            timecount = timecount + 2;
            Gekozentijd.Items.Add($"0{timecount}:00");
            timecount += 2;
            Gekozentijd.Items.Add($"0{timecount}:00");
            timecount += 2;
            Gekozentijd.Items.Add($"0{timecount}:00");
            timecount += 2;
            Gekozentijd.Items.Add($"0{timecount}:00");
        }

        

        private void AdjustCalender()
        {
            DP.DisplayDateStart = DateTime.Now;
            DateTime plusone = DateTime.Now.AddDays(1);
            DateTime plustwo = DateTime.Now.AddDays(2);
            DP.BlackoutDates.Add(new CalendarDateRange(DateTime.Now));

            if (plusone.DayOfWeek == DayOfWeek.Saturday)
            {
                DP.BlackoutDates.Add(new CalendarDateRange(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2)));
                DP.DisplayDateEnd = DateTime.Now.AddDays(3);
            }
            else if (plustwo.DayOfWeek == DayOfWeek.Saturday)
            {
                DP.BlackoutDates.Add(new CalendarDateRange(DateTime.Now.AddDays(2), DateTime.Now.AddDays(3)));
                DP.DisplayDateEnd = DateTime.Now.AddDays(4);
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                DP.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, DateTime.Now.AddDays(1)));
                DP.DisplayDateEnd = DateTime.Now.AddDays(2);
            }
            else if (plustwo.DayOfWeek == DayOfWeek.Sunday)
            {
                DP.BlackoutDates.Add(new CalendarDateRange(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2)));
                DP.DisplayDateEnd = DateTime.Now.AddDays(3);
            }
            else if (plusone.DayOfWeek == DayOfWeek.Sunday)
            {
                DP.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, DateTime.Now.AddDays(1)));
                DP.DisplayDateEnd = DateTime.Now.AddDays(2);
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                DP.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, DateTime.Now.AddDays(-1)));
                DP.DisplayDateEnd = DateTime.Now.AddDays(2);
            }
            else
            {
                DP.DisplayDateEnd = DateTime.Now.AddDays(2);
            }
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Book(object sender, RoutedEventArgs e)
        {
            string selectedtime = string.Empty;
            if (DP.SelectedDate == null || string.IsNullOrEmpty(Gekozentijd.Text))
            {
                MessageBox.Show("Vul een datum en een tijd in ");
            }
            else
            {
                foreach (char ch in Gekozentijd.Text)
                {
                    if (ch.Equals(':'))
                    {

                    }
                    else
                    {
                        selectedtime += ch;
                    }
                }

                string selecteddate = DP.SelectedDate.Value.ToShortDateString();
                string twee = string.Empty;
                foreach (char ch in selecteddate)
                {
                    if (ch == '-')
                    {
                    }
                    else
                    {
                        twee += ch;
                    }
                }
                InsertReservation(twee, selectedtime);
            }
        }

        private void Status(object sender, RoutedEventArgs e)
        {
            var label = sender as Label;
            label.Content = $"Status : {status} ";

        }

        private void AantalPersonen(object sender, RoutedEventArgs e)
        {
 
            var label = sender as Label;
            label.Content = $"Aantal Personen : {aantalp}";
        }

        private void Stuur(object sender, RoutedEventArgs e)
        {
            var label = sender as Label;
            label.Content = $"Stuur : {stir}";
        }

        private void BootNiveau(object sender, RoutedEventArgs e)
        {
            var label = sender as Label;
            label.Content = $"Niveau : {bootniveau}";
        }

        private void AvailibleFrom(object sender, RoutedEventArgs e)
        {
            //get time from database
        }
        public void Getendtime(string rs)
        {

            string str2 = string.Empty;
            int val = 0;
            for (int i = 0; i < rs.Length; i++)
            {
                if (Char.IsDigit(rs[i]))
                    str2 += rs[i];
            }
            if (str2.Length > 0)
            {
                val = int.Parse(str2);

            }
             val += 0200;
            reservationendtime = val.ToString();
        }
    
        public void InsertReservation(string reservationdate, string reservationtime)
        {
            MessageBoxResult result;
            GetReservationID();
            Getendtime(reservationtime);
            try
            {
                
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "127.0.0.1";
                builder.UserID = "SA";
                builder.Password = "Havermout1325";
                builder.InitialCatalog = "Bootverhuur";
                //(convert(DATETIME, {reservationdate},103)))
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    //reservation for now string dont know how else
                    String sql = $"insert into reservation values ({reservationId}, {Id},{reservationdate} , 0{reservationtime}, {reservationendtime}, GetDate())";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            result = MessageBox.Show("Reservering is aangemaakt", "SUCCES");
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void GetReservationID()
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
                    String sql = $"select count (reservation_id) from reservation";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reservationId = reader.GetInt32(0);
                            }
                        }
                    }
                }
                reservationId += 1;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }



        public void Checkeverything(int id)
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
                    String sql = $"SELECT * FROM boat where boat_id = {id}";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               aantalp = reader.GetInt32(1);
                                bootniveau = reader.GetString(2);
                                stir = reader.GetBoolean(3);
                                status = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }

        private void backclick(object sender, RoutedEventArgs e)
        {
            Temp tp = new Temp();
            tp.Show();
            Close();
        }
    }
}
