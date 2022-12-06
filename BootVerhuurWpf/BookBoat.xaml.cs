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


namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for BookBoat.xaml
    /// </summary>
    public partial class BookBoat : Window
    {
        public BookBoat()
        {
            InitializeComponent();
            AdjustCalender();
            AdjustTimeBox();

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
            //dont know yet if correct
            //add reservation in database
            string selecteddate = DP.SelectedDate.Value.Date.ToShortDateString();
            String selectedtime = ;

            bool Stuur;
            if(stuur.Text.Equals("Wel")) 
            {
                Stuur = true;   
            }else
            {
                Stuur = false;
            }
         
            Boat boat = new Boat(Int32.Parse(Aantalpersonen.Text), Stuur, niveau.Text);

            Reservation reservation = new Reservation(DP.SelectedDate.Value, Gekozentijd.Text, boat);
        }

        private void Status(object sender, RoutedEventArgs e)
        {
            var label = sender as Label;
            label.Content = $"Status : ";

        }

        private void AantalPersonen(object sender, RoutedEventArgs e)
        {
            var label = sender as Label;
            label.Content = $"Aantal Personen : ";
        }

        private void Stuur(object sender, RoutedEventArgs e)
        {
            var label = sender as Label;
            label.Content = $"Stuur : ";
        }

        private void BootNiveau(object sender, RoutedEventArgs e)
        {
            var label = sender as Label;
            label.Content = $"Niveau : ";
        }

        private void AvailibleFrom(object sender, RoutedEventArgs e)
        {
            //get time from database
        }

        private void LidniveauCheck(object sender, RoutedEventArgs e)
        {
            if (LidNiveauC.Text.Equals("D"))
            {
                niveau.Items.Add('D');
 
            }
            else
            {
                niveau.Items.Remove('D');
            }
        }
    }
}
