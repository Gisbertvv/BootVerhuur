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
using System.Security.Cryptography.X509Certificates;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for BookBoat.xaml
    /// </summary>
    public partial class BookBoats : Window
    {
        public int aantalp;
        public string bootniveau;
        public bool stir;
        int Id;
        int reservationId;
        string status;
        string reservationendtime;

        List<string> begintimes = new List<string>();
        List<string> endtimes = new List<string>();

        public BookBoats(int id)
        {
            Id = id;
            InitializeComponent();
            AdjustCalender();
            Checkeverything(Id);
        }

        /// <summary>
        /// if role is Lid
        /// </summary>
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
        private void Book(object sender, RoutedEventArgs e)
        {
            string selectedtime = string.Empty;
            if (DP.SelectedDate == null || string.IsNullOrEmpty(Gekozentijd.Text))
            {
                MessageBox.Show("Vul een datum en een tijd in ");
            }
            else
            {

                string selecteddate = DP.SelectedDate.Value.ToShortDateString();
                InsertReservation(selecteddate, Gekozentijd.Text);
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
            //get date from database
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

            StringBuilder sb = new StringBuilder(val.ToString());
            if (sb.Length == 4)
            {
                sb.Insert(2, ":");
            }
            else
            {
                sb.Insert(1, ":");
            }

            reservationendtime = sb.ToString();
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
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    String sql = $"insert into reservation values ({reservationId}, {Id},'{reservationdate}' , '{reservationtime}', '{reservationendtime}', GetDate())";
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

        private void Getreservationtimes(int id, string reservationdate)
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
                    String sql = $"SELECT * FROM reservation where boat_id = {id} AND reservationDate = '{reservationdate}' ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                begintimes.Add(reader.GetString(3));
                                endtimes.Add(reader.GetString(4));
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
      
        private void AdjustTimeBox()
        {
            Getreservationtimes(Id, DP.SelectedDate.Value.ToShortDateString());
            int hours = 6;
            int minutes = 30;
            /*           Gekozentijd.Items.Add($"{hours}:00");
                       minutes += 30;*/
            while (hours != 17)
            {
                if (minutes == 60)
                {
                    hours += 1;
                    minutes = 0;
                    Gekozentijd.Items.Add($"{hours}:00");
                }
                else
                {
                    Gekozentijd.Items.Add($"{hours}:{minutes}");
                }
                minutes += 30;
            }
            hours = 0;
            minutes = 0;



            int i = 0;
            int j = 0;
            string h = string.Empty;
            string m = string.Empty;
            string check = string.Empty;
            while (begintimes.Count > 0)
            {
                string[] times = begintimes.First().Split(':');
                List<string> list = new List<string>(times);
                h = list[0];
                m = list[1];
                list.RemoveAt(0);
                list.RemoveAt(0);
                /*                foreach (char ch in begintimes[j])
                                {
                                    if (ch.Equals(':'))
                                    {
                                        if (i == 1)
                                        {
                                            i += 1;
                                        }
                                        else
                                        {
                                            i += 2;
                                        }                  
                                    }

                                    else if (i == 0 || i == 1)
                                    {
                                         h += ch;
                                        i += 1;
                                    }
                                    else if (i == 2 || i == 3)
                                    {
                                        m += ch;
                                        i += 1;
                                    }

                                }*/
                hours = Int32.Parse(h);
                minutes = Int32.Parse(m);

                Gekozentijd.Items.Remove($"{hours}:{minutes}0");
                
                while (!check.Equals(endtimes.First()))
                {
                    if (minutes.ToString().EndsWith("60"))
                    {

                        minutes = 0;
                        hours += 1;
                        Gekozentijd.Items.Remove($"{hours}:00");
                        check = $"{hours}:00";
                    }
                    else if (minutes.ToString().EndsWith("30"))
                    {
                        Gekozentijd.Items.Remove($"{hours}:{minutes}");
                        check = $"{hours}:{minutes}";
                        minutes += 30;



                    }
                    else if (minutes.ToString().EndsWith("0"))
                    {
                        minutes += 30;
                        Gekozentijd.Items.Remove($"{hours}:{minutes}");
                        check = $"{hours}:{minutes}";
                    }


                    if (hours.ToString().Length == 1 && minutes.ToString().Length == 1)
                    {
                        check = $"0{hours}:{minutes}0";
                    }
                    else if(hours.ToString().Length == 1 && minutes.ToString().Length == 2)
                    {
                        check = $"0{hours}:{minutes}";
                    }
                    else if (minutes.ToString().Length == 1 && hours.ToString().Length == 2)
                    {
                        check = $"{hours}:{minutes}0";
                    }


                }
                begintimes.Remove(begintimes.First());
                endtimes.Remove(endtimes.First());
            }
        }
        private void SelectionDatechanged(object sender, SelectionChangedEventArgs e)
        {          
            AdjustTimeBox();
        }

        private void AccidentReport(object sender, RoutedEventArgs e)
        {
            PDFWindow window = new PDFWindow();
            window.Show();
        }

        private void OpenReservePanel(object sender, RoutedEventArgs e)
        {
            Temp tp = new Temp();
            tp.Show();
            Close();
        }
        private void Logout(object sender, RoutedEventArgs e)
        {
            Login window = new Login();
            window.Show();
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Temp tp = new Temp();
            tp.Show();
            Close();
        }
    }
}

