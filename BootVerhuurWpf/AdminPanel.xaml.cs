using BoldReports.RDL.DOM;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
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
                SqlConnection connection = new SqlConnection(_builder.ConnectionString);
                using (connection)
                {
                    //SQL quary
                    String sql = "UPDATE primary_color from appSettings";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            /*while (reader.Read())
                            {
                                if (username.Equals(reader.GetString(0)))
                                {
                                    connection.Close();
                                    return true;
                                }

                                if (email.Equals(reader.GetString(1)))
                                {
                                    connection.Close();
                                    return true;
                                }
                            }*/
                        }

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
                SqlConnection connection = new SqlConnection(_builder.ConnectionString);
                using (connection)
                {
                    //SQL quary
                    String sql = "UPDATE secondary_color from appSettings";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.
                            /*while (reader.Read())
                            {
                                if (username.Equals(reader.GetString(0)))
                                {
                                    connection.Close();
                                    return true;
                                }

                                if (email.Equals(reader.GetString(1)))
                                {
                                    connection.Close();
                                    return true;
                                }
                            }*/
                        }

                        connection.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                System.Windows.MessageBox.Show(e.ToString());
            }
        }
    }
}
    