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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AccidentReportWindow : Window
    {
        public AccidentReportWindow()
        {
            InitializeComponent();
        }

        private void TopLeftButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ThirdParty_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void createAccidentReport(object sender, RoutedEventArgs e)
        {
            string Name;
            DateTime datetime;
            string WeatherConditions;
            string Place;

            string Involvement;
            string DamageOccurd;

            string NameBoat;
            string AsociationName;
            string occupants;

            string ObjectName;
            string ExternalPartyAssociation;
            string ExternalPartyOccupants;

            string OpinionHow;
            string Opinioncause;
            string OpinionPrevent;
            string OpinionGuilt;

            string applyToMyBoat;
            string applyToExternalObject;

            string ThirdPartyInfo;

            //image stituatie schets
            //image schade fotos

            //image / tekening
            string location;
            DateTime singatureDate;


            Name = nameInput.Text;

            WeatherConditions = weatherConditionsInput.Text;
            Place = placeInput.Text;
            
            Involvement = weatherConditionsInput.Text;
            DamageOccurd = weatherConditionsInput.Text;
            if (RadioButton1.IsChecked == true)
            {
                Involvement = RadioButton1.Content.ToString();
            }else if (RadioButton2.IsChecked == true)
            {
                Involvement = RadioButton2.Content.ToString();
            }else if (RadioButton3.IsChecked == true)
            {
                Involvement = RadioButton3.Content.ToString();
            }else if (RadioButton4.IsChecked == true)
            {
                Involvement = RadioButton4.Content.ToString();
            }else if (RadioButton5.IsChecked == true)
            {
                Involvement = RadioButton5.Content.ToString();
            }else if (RadioButton6.IsChecked == true)
            {
                Involvement = RadioButton6.Content.ToString();
            }else if (RadioButton7.IsChecked == true)
            {
                Involvement = RadioButton7.Content.ToString();
            }else if (RadioButton8.IsChecked == true)
            {
                Involvement = RadioButton8.Content.ToString();
            }else if (RadioButton9.IsChecked == true)
            {
                Involvement = RadioButton9.Content.ToString();
            }else if (RadioButton10.IsChecked == true)
            {
                Involvement = RadioButton10.Content.ToString();
            }



            if (RadioButton11.IsChecked == true)
            {
                DamageOccurd = RadioButton11.Content.ToString();
            }
            else if (RadioButton12.IsChecked == true)
            {
                DamageOccurd = RadioButton12.Content.ToString();
            }
            else if (RadioButton13.IsChecked == true)
            {
                DamageOccurd = RadioButton13.Content.ToString();
            }
            else if (RadioButton14.IsChecked == true)
            {
                DamageOccurd = RadioButton14.Content.ToString();
            }
            else if (RadioButton15.IsChecked == true)
            {
                DamageOccurd = RadioButton15.Content.ToString();
            }
            else if (RadioButton16.IsChecked == true)
            {
                DamageOccurd = RadioButton16.Content.ToString();
            }
            else if (RadioButton17.IsChecked == true)
            {
                DamageOccurd = RadioButton17.Content.ToString();
            }
            else if (RadioButton18.IsChecked == true)
            {
                DamageOccurd = RadioButton18.Content.ToString();
            }
            MessageBox.Show(DamageOccurd);
        }
    }
}
