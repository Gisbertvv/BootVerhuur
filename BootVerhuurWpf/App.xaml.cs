
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
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

            AdminPanel panel= new AdminPanel();
            Color color = (Color)ColorConverter.ConvertFromString(panel.GetColors()[0]);
            SolidColorBrush solidColorBrush = new SolidColorBrush(color);
            Resources.Add("PrimairyColor", solidColorBrush);
            Color colorSecundary = (Color)ColorConverter.ConvertFromString(panel.GetColors()[1]);
            SolidColorBrush solidColorBrushSecundary = new SolidColorBrush(colorSecundary);
            Resources.Add("SecundaryColor", solidColorBrushSecundary);

        }
    }
}
