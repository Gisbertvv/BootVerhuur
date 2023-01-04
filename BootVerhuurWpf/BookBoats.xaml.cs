using Microsoft.Identity.Client;
using Syncfusion.Windows.Controls.Grid;
using Syncfusion.XlsIO.Implementation.XmlSerialization;
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
        Zon sunRiseSet;
        string sunrise = Zon.sun_rise;
        string sunset = Zon.sun_set;
        int beginhour = 0;
        int beginminutes = 0;
        int endhour = 0;
        int endminutes = 0;

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
            sunRiseSet = new Zon();
            sunrise = Zon.sun_rise;
            sunset = Zon.sun_set;
            GetBeginAndEndTimes(sunrise, sunset);
        }
        /// <summary>
        /// puts the begin and endtimes from sunrise and sunset into integers
        /// </summary>
        /// <param name="sunrise"></param>
        /// <param name="sunset"></param>
        private void GetBeginAndEndTimes(string sunrise, string sunset)
        {
            string[] words = (sunrise.Split(':'));
            string[] words2 = (sunset.Split(':'));
            beginhour= int.Parse(words[0]);
            beginminutes= int.Parse(words[1]);

            endhour= int.Parse(words2[0]);          
            endhour -=2;
            endminutes= int.Parse(words2[1]);

            //round down to the closest halfhour or hour
            while (endminutes != 30)
            {
                if (endminutes == 0)
                {
                    break;
                }
                else
                {
                    endminutes--;
                }
            }
            //round up to the closest halfhour or hour.
            while (beginminutes != 30)
            {
                if (beginminutes == 60)
                {
                    beginhour += 1;
                    beginminutes = 0;
                    break;
                }
                else
                {
                    beginminutes++;
                }
            }
        }
    
        /// <summary>
        /// Adjust the Calender to display two days ahead. 
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

        /// <summary>
        /// When button is pressed inserts reservation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Sets the label to the status of the boat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Status(object sender, RoutedEventArgs e)
        {
            var label = sender as Label;
            label.Content = $"Status : {status} ";
        }
        /// <summary>
        /// Sets the label to the capacity of the boat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AantalPersonen(object sender, RoutedEventArgs e)
        {
            var label = sender as Label;
            label.Content = $"Aantal Personen : {aantalp}";
        }
        /// <summary>
        /// Sets the label to the stir boolean of the boat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stuur(object sender, RoutedEventArgs e)
        {
            var label = sender as Label;
            label.Content = $"Stuur : {stir}";
        }
        /// <summary>
        /// Sets the label to the level of the boat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BootNiveau(object sender, RoutedEventArgs e)
        {
            var label = sender as Label;
            label.Content = $"Niveau : {bootniveau}";
        }

        /// <summary>
        /// calculates the endtime from begintime
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
            int bb = 10;
            int mm = 8;
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
            int tempbeginhour = beginhour;
            int tempbeginminutes = beginminutes;
            Gekozentijd.Items.Clear();
            Alltimes.Clear();
            Reservedtimes.Clear();
            // is the endtime need to take 2 hours because cant row when dark.
            while (!$"{tempbeginhour}:{tempbeginminutes}".Equals($"{endhour}:{endminutes}"))
            {
                if (tempbeginhour.ToString().Length == 1)
                {
                    if (tempbeginminutes.ToString().Equals("0"))
                    {
                        Alltimes.Add($"0{tempbeginhour}:{tempbeginminutes}0");
                        tempbeginminutes += 15;
                    }
                    else if (tempbeginminutes.ToString().Equals("15"))
                    {
                        Alltimes.Add($"0{tempbeginhour}:{tempbeginminutes}");
                        tempbeginminutes += 15;
                    }
                    else if (tempbeginminutes.ToString().Equals("30"))
                    {
                        Alltimes.Add($"0{tempbeginhour}:{tempbeginminutes}");
                        tempbeginminutes += 15;
                    }
                    else if (tempbeginminutes.ToString().Equals("45"))
                    {
                        Alltimes.Add($"0{tempbeginhour}:{tempbeginminutes}");
                        tempbeginhour += 1;
                        tempbeginminutes = 0;
                    }
                }
                else if (tempbeginhour.ToString().Length == 2)
                {
                    if (tempbeginminutes.ToString().Equals("0"))
                    {
                        Alltimes.Add($"{tempbeginhour}:{tempbeginminutes}0");
                        tempbeginminutes = 15;
                    }
                    else if (tempbeginhour.ToString().Length == 2 && tempbeginminutes.ToString().Equals("15"))
                    {
                        Alltimes.Add($"{tempbeginhour}:{tempbeginminutes}");
                        tempbeginminutes += 15;

                    }
                    else if (tempbeginhour.ToString().Length == 2 && tempbeginminutes.ToString().Equals("30"))
                    {
                        Alltimes.Add($"{tempbeginhour}:{tempbeginminutes}");
                        tempbeginminutes += 15;
                    }
                    else if (tempbeginhour.ToString().Length == 2 && tempbeginminutes.ToString().Equals("45"))
                    {
                        Alltimes.Add($"{tempbeginhour}:{tempbeginminutes}");
                        tempbeginminutes = 0;
                        tempbeginhour += 1;
                    }

                }
            }
            Minimum();
            foreach (string s in Alltimes)
            {
                Gekozentijd.Items.Add(s);
            }
        }
        /// <summary>
        /// When different date is selected reserved times are deleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            PDFWindow window = new PDFWindow();
            window.Show();
        }

        private void OpenReservePanel(object sender, RoutedEventArgs e)
        {
            Temp temp = new Temp();
            temp.Show();
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
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

        private void Open_MemberReservations(object sender, RoutedEventArgs e)
        {
            MemberReservations memberReservations= new MemberReservations();    
            memberReservations.Show();
            Close();
        }
    }
}

