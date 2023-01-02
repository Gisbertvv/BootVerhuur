using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for BookBoat.xaml
    /// </summary>
    public partial class BookBoats : Window
    {
        public string status;
        public int aantalp;
        public string bootniveau;
        public bool stir;
        int Id;

        string reservationendtime;
        string date1;
        string date2;
        string selecteddate;


        List<string> begintimes = new List<string>();
        List<string> endtimes = new List<string>();
        List<string> Alltimes = new List<string>();
        List<string> Reservedtimes = new List<string>();
        Bookboat bookboat;

        public BookBoats(int id)
        {
            Id = id;
            InitializeComponent();
            AdjustCalender();
            bookboat = new Bookboat(date1, date2);
            bookboat.Checkeverything(Id);

            status = bookboat.status;
            aantalp = bookboat.aantalp;
            bootniveau = bookboat.bootniveau;
            stir = bookboat.stir;

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


        private void Book(object sender, RoutedEventArgs e)
        {
            if (DP.SelectedDate == null || string.IsNullOrEmpty(Gekozentijd.Text))
            {
                MessageBox.Show("Vul een datum en een tijd in ");
            }
            else
            {
                Getendtime(Gekozentijd.Text);
                if (bookboat.InsertReservation(selecteddate, Gekozentijd.Text, reservationendtime))
                {
                    begintimes = bookboat.begintimes;
                    endtimes = bookboat.endtimes;
                    AdjustTimeBox();
                }
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
                sb.Insert(0, "0");
                sb.Insert(2, ":");
            }

            reservationendtime = sb.ToString();
        }

        private void backclick(object sender, RoutedEventArgs e)
        {
            Temp tp = new Temp();
            tp.Show();
            Close();
        }

        /// <summary>
        /// deletes the unavailible timeslots out of the combobox
        /// </summary>
        private void AdjustTimeBox()
        {
            int indexb;
            int ee = 9;
            int bb = 5;
            int mm = 4;
            SetTimeBox();

            while (begintimes.Count > 0)
            {
                try
                {
                    int indexofbegintimes = Alltimes.IndexOf(begintimes[0]);
                    int beginindex = indexofbegintimes - mm;
                    int endindex = indexofbegintimes + bb;

                    while (beginindex != endindex)
                    {
                        if (!Reservedtimes.Contains(Alltimes[beginindex]))
                        {
                            Reservedtimes.Add(Alltimes[beginindex]);
                            beginindex++;
                        }
                        else
                        {
                            beginindex++;
                        }
                    }

                    begintimes.RemoveAt(0);
                    endtimes.RemoveAt(0);
                }

                catch (Exception ex)
                {
                    mm--;
                }

            }

            Alltimes = Alltimes.Except(Reservedtimes).ToList();
            Gekozentijd.Items.Clear();

            foreach (string s in Alltimes)
            {
                Gekozentijd.Items.Add(s);
            }
        }

        /// <summary>
        /// rounds the time up to the closest half hour or hour
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        string RoundMinutes(DateTime Input)
        {
            DateTime Output;
            int Minute;
                Minute = +1;

            while ((Input.Minute != 0) && (Input.Minute != 30))
                Input = Input.AddMinutes(Minute);

            return Input.ToShortTimeString();
        }

        /// <summary>
        /// Deletes all the times that fall in the 24 hours constraint
        /// </summary>
        private void Minimum()
        {
            try
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                {
                }
                else if (selecteddate.Equals(date1))
                {
                    int index = Alltimes.IndexOf(RoundMinutes(DateTime.Now));
                    Alltimes.RemoveRange(0, index);
                }
            }
            catch(ArgumentOutOfRangeException ae) 
            { 
                Alltimes.Clear();
            }

        }
        /// <summary>
        /// fills the combobox with times with half hour between
        /// </summary>
                private void SetTimeBox()
                {
            Gekozentijd.Items.Clear();
            Alltimes.Clear();
            Reservedtimes.Clear();
           //begintime
            int hours = 6;
            int minutes = 0;
            // is the endtime need to take 2 hours because cant row when dark.
            while (hours != 16)
            {
                if(hours.ToString().Length == 1 && minutes.ToString().Length == 1)
                {
                    Alltimes.Add($"0{hours}:{minutes}0");
                    minutes+= 30;
                }
                else if (hours.ToString().Length == 1 && minutes.ToString().Length ==2)
                {
                    Alltimes.Add($"0{hours}:{minutes}");
                    hours += 1;
                    minutes= 0;
                }
                else if(hours.ToString().Length == 2&&minutes.ToString().Equals("0"))
                {
                    Alltimes.Add($"{hours}:{minutes}0");
                    minutes = 30;                   
                }
                else if (hours.ToString().Length == 2 && minutes.ToString().Equals("30"))
                {
                    Alltimes.Add($"{hours}:{minutes}");
                    minutes = 0;
                    hours += 1;
                }
               
            }
            Minimum();
            foreach(string s in Alltimes)
            {
                Gekozentijd.Items.Add(s);   
            }
        }

        private void SelectionDatechanged(object sender, SelectionChangedEventArgs e)
        {
            selecteddate = DP.SelectedDate.Value.ToShortDateString();
            bookboat.Getreservationtimes(Id, DP.SelectedDate.Value.ToShortDateString());
            begintimes = bookboat.begintimes;
            endtimes = bookboat.endtimes;
            AdjustTimeBox();
        }

        private void AccidentReport(object sender, RoutedEventArgs e)
        {

        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            Create create = new Create();
            create.Show();
            Close();
        }

        private void Open_AdminPanel(object sender, RoutedEventArgs e)
        {
            AdminPanel adminpanel = new AdminPanel();
            adminpanel.Show();
            Close();
        }
        private void Logout(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Backclick(object sender, RoutedEventArgs e)
        {
            Temp tp = new Temp();
            tp.Show();
            Close();
        }
    }
}

