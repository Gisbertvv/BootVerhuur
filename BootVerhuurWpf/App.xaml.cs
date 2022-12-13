using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        public string primaryColor { get; set; }
        public string secondaryColor { get; set; }

        public App()
        {
           // AdminPanel panel = new AdminPanel();

            //primaryColor = panel.GetColors()[0];
            //secondaryColor = panel.GetColors()[1];

            //int color = Convert.ToInt32("FFFFFF", 16);


            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Nzc2NjE5QDMyMzAyZTMzMmUzMGd1bDJuL0U4UUZJOVpmZVlBWjYvSU9uazROWXJITUg4blFINmc0SEtEaE09");

           //this.Resources["ButtonExitBackgroundColor"] = new SolidColorBrush();  
           // this.Resources["ButtonExitBorderColor"] = new SolidColorBrush(Colors.Black);
        }
    }
}
