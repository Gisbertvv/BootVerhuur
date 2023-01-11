
using System.Windows;
using System.Windows.Media;



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

            //Converters color from string to solidcolorbrush
            Color color = (Color)ColorConverter.ConvertFromString(panel.GetColors()[0]);
            SolidColorBrush solidColorBrush = new SolidColorBrush(color);

            //Sets primary color
            Resources.Add("PrimairyColor", solidColorBrush);

            //Converters color from string to solidcolorbrush
            Color colorSecundary = (Color)ColorConverter.ConvertFromString(panel.GetColors()[1]);
            SolidColorBrush solidColorBrushSecundary = new SolidColorBrush(colorSecundary);

            //Sets secondary color
            Resources.Add("SecondaryColor", solidColorBrushSecundary);

            //Converters color from string to solidcolorbrush
            Color colorBackground = (Color)ColorConverter.ConvertFromString(panel.GetColors()[2]);
            SolidColorBrush solidColorBrushBackground = new SolidColorBrush(colorBackground);
            //Sets background color
            Resources.Add("BackgroundColor", solidColorBrushBackground);

        }
    }
}
