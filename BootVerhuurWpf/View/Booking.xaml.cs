using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using BootVerhuurWpf.Model;
using CoordinateSharp;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for BookBoat.xaml
    /// </summary>
    public partial class Booking : Window
    {
        public string status;
        public int numberOfPeople;
        public string boatingLevel;
        public bool steeringWheel;
        int id;

        string reservationendtime;
        string date1;
        string date2;
        string selecteddate;

        List<string> begintimes = new List<string>();
        List<string> endtimes = new List<string>();
        List<string> Alltimes = new List<string>();
        List<string> Reservedtimes = new List<string>();
        RentalController rentalboat;
        Sun sunRiseSet;
        string sunrise = Sun.sun_rise;
        string sunset = Sun.sun_set;
        int beginhour = 0;
        int beginminutes = 0;
        int endhour = 0;
        int endminutes = 0;

        public Booking(int id)
        {
            this.id = id;
            InitializeComponent();
            AdjustCalender();
            rentalboat = new RentalController(date1, date2);
            rentalboat.CheckEverything(this.id);

            status = rentalboat.Status;
            numberOfPeople = rentalboat.NumberOfPeople;
            boatingLevel = rentalboat.BoatLevel;
            steeringWheel = rentalboat.SteeringWheel;

            //creates sunset en sunrise times for a given location
            //using the CoordinateSharp library
            Celestial cel = Celestial.CalculateCelestialTimes(52.2253678, 5.4875229, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
            //add 1 hour to the time because the library is in UTC
            sunrise = $"{cel.SunRise.Value.Hour + 1}:{cel.SunRise.Value.Minute}:{cel.SunRise.Value.Second}";
            //add 1 hour to the time because the library is in UTC
            sunset = $"{cel.SunSet.Value.Hour + 1}:{cel.SunSet.Value.Minute}:{cel.SunSet.Value.Second}";

            MessageBox.Show($"{sunrise}  {sunset}");

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
        /// When button is pressed inserts reservation into the database
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
                GetEndTime(Gekozentijd.Text);
                if (rentalboat.InsertReservation(selecteddate, Gekozentijd.Text, reservationendtime))
                {
                    begintimes = rentalboat.begintimes;
                    endtimes = rentalboat.endtimes;
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
        private void NumberOfPeople(object sender, RoutedEventArgs e)
        {
            var label = sender as Label;
            label.Content = $"Aantal Personen : {numberOfPeople}";
        }
        /// <summary>
        /// Sets the label to the stir boolean of the boat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SteeringWheel(object sender, RoutedEventArgs e)
        {
            var label = sender as Label;
            label.Content = $"Stuur : {steeringWheel}";
        }
        /// <summary>
        /// Sets the label to the level of the boat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoatLevel(object sender, RoutedEventArgs e)
        {
            var label = sender as Label;
            label.Content = $"Niveau : {boatingLevel}";
        }

        /// <summary>
        /// calculates the endtime from begintime
        /// </summary>
        /// <param name="rs"></param>
        public void GetEndTime(string rs)
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

        private void BackClick(object sender, RoutedEventArgs e)
        {
            List tp = new List();
            tp.Show();
            Close();
        }

        /// <summary>
        /// deletes the unavailible timeslots out of the combobox
        /// </summary>
        private void AdjustTimeBox()
        {
            /*            int indexb;
                        int bb = 10;
                        int mm = 8;*/
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
                     bb = 10;
                     mm = 8;
                }

                catch (Exception ex)
                {
                    if (Alltimes.IndexOf(begintimes[0]) == 0)
                    {
                        mm =0;
                    }
                    else if ((Alltimes.IndexOf(begintimes[0])-mm) < 0)
                    {
                        mm--;
                    }
                    else
                    {
                        begintimes.RemoveAt(0);
                        endtimes.RemoveAt(0);
                    }
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
        /// rounds the time up to the closest 15, 30,45 minutes or hour 
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        string RoundMinutes(DateTime Input)
        {
            int Minute;
                Minute = +1;

            while ((Input.Minute != 0) && (Input.Minute != 15) && (Input.Minute != 30) && (Input.Minute != 45))
                Input = Input.AddMinutes(Minute);

            return Input.ToShortTimeString();
        }

        /// <summary>
        /// Deletes all the times that fall in the 24 hours constraint if the date is tommorow
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
        /// fills the combobox with times with 15 minutes between
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
            rentalboat.GetReservationTimes(id, DP.SelectedDate.Value.ToShortDateString());
            begintimes = rentalboat.begintimes;
            endtimes = rentalboat.endtimes;
            AdjustTimeBox();
        }

        private void AccidentReport(object sender, RoutedEventArgs e)
        {
            PDF window = new PDF();
            window.Show();
            Close();
        }

        private void OpenReservePanel(object sender, RoutedEventArgs e)
        {
            List temp = new List();
            temp.Show();
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Settings adminpanel = new Settings();
            adminpanel.Show();
            Close();
        }
        private void Logout(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Backclick(object sender, RoutedEventArgs e)
        {
            List tp = new List();
            tp.Show();
            Close();
        }

        private void Open_MemberReservations(object sender, RoutedEventArgs e)
        {
            Reservations memberReservations= new Reservations();    
            memberReservations.Show();
            Close();
        }
    }
}

