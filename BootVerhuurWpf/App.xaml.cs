
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BootVerhuur;


namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application 
    {
           public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Nzc2NjE5QDMyMzAyZTMzMmUzMGd1bDJuL0U4UUZJOVpmZVlBWjYvSU9uazROWXJITUg4blFINmc0SEtEaE09");

            Database database= new Database();
            //Automatic database connection
            
            database.OpenConnnection();
            Colors panel = new Colors();

            //converters color from string to solidcolorbrush
            Color color = (Color)ColorConverter.ConvertFromString(panel.GetColors()[0]);
            SolidColorBrush solidColorBrush = new SolidColorBrush(color);

            //sets primary color
            Resources.Add("PrimairyColor", solidColorBrush);

            //converters color from string to solidcolorbrush
            Color colorSecundary = (Color)ColorConverter.ConvertFromString(panel.GetColors()[1]);
            SolidColorBrush solidColorBrushSecundary = new SolidColorBrush(colorSecundary);

            //sets secondary color
            Resources.Add("SecondaryColor", solidColorBrushSecundary);

            //converters color from string to solidcolorbrush
            Color colorBackground = (Color)ColorConverter.ConvertFromString(panel.GetColors()[2]);
            SolidColorBrush solidColorBrushBackground = new SolidColorBrush(colorBackground);
            //sets background color
            Resources.Add("BackgroundColor", solidColorBrushBackground);

        }
    }
}
