using BoldReports.RDL.DOM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.MessageBox;
using WindowStartupLocation = System.Windows.WindowStartupLocation;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for CreateAdmin.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        private static SqlConnection _builder;
       

      
        public AdminPanel()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Color color = (Color)ColorConverter.ConvertFromString(GetColors()[0]);
            SolidColorBrush solidColorBrush = new SolidColorBrush(color);
            gridje.Background = solidColorBrush;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AdminPanelInfo(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"(R){PrimaryColor.Color.R.ToString()} (G) {PrimaryColor.Color.G.ToString()}  (B) {PrimaryColor.Color.B.ToString()}  (A) {PrimaryColor.Color.A.ToString()}");
            //MessageBox.Show($"(R){SecondaryColor.Color.R.ToString()} (G) {SecondaryColor.Color.G.ToString()}  (B) {SecondaryColor.Color.B.ToString()}  (A) {SecondaryColor.Color.A}");

       
            /*Color myRgbColor = new Color();
            myRgbColor = Color.FromArgb(PrimaryColor.Color.A, PrimaryColor.Color.R, PrimaryColor.Color.G, PrimaryColor.Color.B);
            MessageBox.Show($"{myRgbColor}");

            SolidColorBrush solidColorBrush = new SolidColorBrush(myRgbColor);*/
            SetThemeColors(PrimaryColor.Color.ToString(), SecondaryColor.Color.ToString());

            if (PrimaryColor != null && SecondaryColor != null)
            {
                SetThemeColors(PrimaryColor.Color.ToString(), SecondaryColor.Color.ToString());
            }
            else if (SecondaryColor != null)
            {
                SetPrimaryColor(PrimaryColor.Color.ToString());
            }
            else if (PrimaryColor != null)
            {
                SetSecondaryColor(SecondaryColor.Color.ToString());
            }
          

        }

        public static void SetThemeColors(string PrimaryColor, string SecondaryColor)
        {
            SetPrimaryColor(PrimaryColor);
            SetSecondaryColor(SecondaryColor);
        }

        private static void SetPrimaryColor(string PrimaryColor)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "127.0.0.1";
                builder.UserID = "SA";
                builder.Password = "Havermout1325";
                builder.InitialCatalog = "BootVerhuur";
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                using (connection)
                {
                    //SQL query
                    String sql = $"UPDATE appSettings SET primary_color='{PrimaryColor}'";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                System.Windows.MessageBox.Show(e.ToString());
            }
        }

        private static void SetSecondaryColor(string SecondaryColor)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "127.0.0.1";
                builder.UserID = "SA";
                builder.Password = "Havermout1325";
                builder.InitialCatalog = "BootVerhuur";
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                using (connection)
                {
                    //SQL query
                    String sql = $"UPDATE appSettings SET secondary_color ='{SecondaryColor}'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                System.Windows.MessageBox.Show(e.ToString());
            }
        }

        public string[] GetColors()
        {
            string[] color = null;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "127.0.0.1";
                builder.UserID = "SA";
                builder.Password = "Havermout1325";
                builder.InitialCatalog = "BootVerhuur";

                string color1;
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                using (connection)
                {
                    //SQL quary
                    String sql = "SELECT primary_color, secondary_color FROM appSettings";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                 color = new string[] { reader.GetString(0), reader.GetString(1)};
                            }
                        }
                        connection.Close();
                    }
                }
            }

            catch (SqlException e)
            {
               MessageBox.Show(e.ToString());
            }

            return color;
        }
    }
}
    

