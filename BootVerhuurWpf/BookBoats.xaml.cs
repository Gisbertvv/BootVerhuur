﻿using System;
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
using Syncfusion.DocIO.DLS;

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
        string date1;
        string date2;
        string selecteddate;
        static int memberId = Int32.Parse(Login.id);

        List<string> begintimes = new List<string>();
        List<string> endtimes = new List<string>();
        List<string> Alltimes = new List<string>();

        public BookBoats(int id)
        {
            Id = id;
            InitializeComponent();
            AdjustCalender();
            Checkeverything(Id);

        }
       
        /// <summary>
        /// if role is Lid only see two days ahead for reservation and no reservation for the weekend
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
                date1 = DateTime.Now.AddDays(3).ToShortDateString();
                date2 = DateTime.Now.AddDays(4).ToShortDateString();
            }
            else if (plustwo.DayOfWeek == DayOfWeek.Saturday)
            {
                DP.BlackoutDates.Add(new CalendarDateRange(DateTime.Now.AddDays(2), DateTime.Now.AddDays(3)));
                DP.DisplayDateEnd = DateTime.Now.AddDays(4);
                date1 = DateTime.Now.AddDays(1).ToShortDateString();
                date2 = DateTime.Now.AddDays(4).ToShortDateString();
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                DP.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, DateTime.Now.AddDays(1)));
                DP.DisplayDateEnd = DateTime.Now.AddDays(2);
                date1 = DateTime.Now.AddDays(2).ToShortDateString();
                date2 = DateTime.Now.AddDays(3).ToShortDateString();
            }
            else if (plustwo.DayOfWeek == DayOfWeek.Sunday)
            {
                DP.BlackoutDates.Add(new CalendarDateRange(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2)));
                DP.DisplayDateEnd = DateTime.Now.AddDays(3);
                date1 = DateTime.Now.AddDays(3).ToShortDateString();
                date2 = DateTime.Now.AddDays(4).ToShortDateString();
            }
            else if (plusone.DayOfWeek == DayOfWeek.Sunday)
            {
                DP.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, DateTime.Now.AddDays(1)));
                DP.DisplayDateEnd = DateTime.Now.AddDays(2);
                date1 = DateTime.Now.AddDays(2).ToShortDateString();
                date2 = DateTime.Now.AddDays(3).ToShortDateString();
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                DP.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, DateTime.Now.AddDays(-1)));
                DP.DisplayDateEnd = DateTime.Now.AddDays(2);
                date1 = DateTime.Now.AddDays(1).ToShortDateString();
                date2 = DateTime.Now.AddDays(2).ToShortDateString();
            }
            else
            {
                DP.DisplayDateEnd = DateTime.Now.AddDays(2);
                date1 = DateTime.Now.AddDays(1).ToShortDateString();
                date2 = DateTime.Now.AddDays(2).ToShortDateString();
            }
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Book(object sender, RoutedEventArgs e)
        {
            if (DP.SelectedDate == null || string.IsNullOrEmpty(Gekozentijd.Text))
            {
                MessageBox.Show("Vul een datum en een tijd in ");
            }
            else
            {
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

        /// <summary>
        /// calculates the endtime from begintimes
        /// </summary>
        /// <param name="rs"></param>
        public void Getendtime(string rs)
        {
            //get all the ints from the string
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
            //add 2 hours
            val += 0200;

            //change back to string with : 
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
        int reservationCount = 0;
        public void GetMemberIdCountReservations(string reservationdate, string reservationdate2)
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
                    String sql = $"Select count(*) from reservation where member_id = {memberId} and reservationDate = '{reservationdate}' or reservationDate = '{reservationdate2}'";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reservationCount = reader.GetInt32(0);

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
        
        public void InsertReservation(string reservationdate, string reservationtime)
        {
            try
            {
                    GetMemberIdCountReservations(date1, date2);
                
                if (reservationCount == 2)
                {
                    MessageBox.Show("Je kunt maximaal 2 reserveringen hebben");
                }
                else
                {
                    {
                        GetReservationID();
                        Getendtime(reservationtime);

                        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                        builder.DataSource = "127.0.0.1";
                        builder.UserID = "SA";
                        builder.Password = "Havermout1325";
                        builder.InitialCatalog = "Bootverhuur";
                        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                        {
                            String sql = $"insert into reservation values ({reservationId}, {Id},'{reservationdate}' , '{reservationtime}', '{reservationendtime}', GetDate(),{memberId})";
                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                connection.Open();
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    Getreservationtimes(Id,reservationdate);                              
                                    MessageBox.Show("Reservering is aangemaakt", "SUCCES");
                                    
                                }
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

        /// <summary>
        /// puts all the begin and end reservationtimes for a specific boatid and date in a list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reservationdate"></param>
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
                t();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }

        private void t()
        {
                     
          while (begintimes.Count > 0)
            {
                int index = Alltimes.IndexOf(begintimes[0]);
                try
                {
                    int before = index- 4;
                    
                    Alltimes.RemoveRange(before, index);
                    begintimes.RemoveAt(0);                  
                }

                catch(ArgumentOutOfRangeException ae)
                {
                    break;
                }
           
            }
           
           Gekozentijd.Items.Clear();

            foreach (string s in Alltimes)
            {
                Gekozentijd.Items.Add(s);
            }
        }

        /// <summary>
        /// rounds the time to the closest half hour or hour
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        string RoundMinutes(DateTime Input)
        {
            DateTime Output;
            int Minute;
           

/*            if ((Input.Minute <= 15) || ((Input.Minute > 30) && (Input.Minute <= 45)))
                Minute = -1;
            else*/
                Minute = +1;

            while ((Input.Minute != 0) && (Input.Minute != 30))
                Input = Input.AddMinutes(Minute);

            return Input.ToShortTimeString();
        }

        private void Minimum()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {               
            }
            else if(selecteddate.Equals(date1))
            {
                int index = Alltimes.IndexOf(RoundMinutes(DateTime.Now));
                Alltimes.RemoveRange(0, index);
            }
        }

                private void SetTimeBox()
                {
            Gekozentijd.Items.Clear();
            Alltimes.Clear();
            int hours = 6;
            int minutes = 0;
            if (minutes.ToString().EndsWith('0'))
            {
                Gekozentijd.Items.Add($"{hours}:00");
                Alltimes.Add($"{hours}:00");
                minutes += 30;
            }
            // is the endtime need to take 2 hours because cant row when dark.
            while (hours != 18)
            {
                if (minutes == 60)
                {
                    hours += 1;
                    minutes = 0;
                    Alltimes.Add($"{hours}:00");
                }
                else
                {
                    Alltimes.Add($"{hours}:{minutes}");
                }
                minutes += 30;
            }
            foreach(string s in Alltimes)
            {
                Gekozentijd.Items.Add(s);   
            }
        }

        private void SelectionDatechanged(object sender, SelectionChangedEventArgs e)
        {
            selecteddate = DP.SelectedDate.Value.ToShortDateString();
            SetTimeBox();
            Minimum();
            Getreservationtimes(Id, DP.SelectedDate.Value.ToShortDateString());

        }

        private void AccidentReport(object sender, RoutedEventArgs e)
        {

        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Open_AdminPanel(object sender, RoutedEventArgs e)
        {

        }

        private void Backclick(object sender, RoutedEventArgs e)
        {
            Temp tp = new Temp();
            tp.Show();
            Close();
        }
    }
}

