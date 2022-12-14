using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using Syncfusion.Windows.Shared;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for CreateAdmin.xaml
    /// </summary>
    public partial class CreateAdmin : Window
    {
        MainWindow mainWindow = new MainWindow();
        public Admin admin;
        bool digits = false;
        bool special = false;


        public CreateAdmin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private static bool IsEmailValid(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }

        private bool ContainsDigit(string wachtwoord)
        {
            foreach (char l in wachtwoord)
            {
                if (Char.IsDigit(l))
                {
                    digits = true;
                }

                if (digits)
                {
                    break;
                }
            }

            return digits;
        }

        private bool ContainsSpecial(string wachtwoord)
        {
            string regexItem = @"\|!#$%&/+-()=?»«@£§€{}.-;'<>_,";

            foreach (var item in regexItem)
            {
                if (wachtwoord.Contains(item))
                {
                    special = true;
                }

                if (special)
                {
                    break;
                }
            }

            return special;
        }


        private void Create_Admin(object sender, RoutedEventArgs e)
        {
            string messageBoxText;
            string caption;
            MessageBoxButton button;
            MessageBoxImage icon;
            MessageBoxResult result;
            ContainsDigit(txtWachtwoord.Password.ToString());
            ContainsSpecial(txtWachtwoord.Password.ToString());

            if (string.IsNullOrWhiteSpace(txtGebruikersnaam.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtWachtwoord.Password.ToString()))
            {
                messageBoxText = "Alle velden moeten ingevuld zijn";
                caption = "FAILED: Één of meerdere velden zijn leeg";
                button = MessageBoxButton.OK;
                icon = MessageBoxImage.Error;
            }
            else if (!IsEmailValid(txtEmail.Text) ||
                     (!txtEmail.Text.EndsWith(".nl") && !txtEmail.Text.EndsWith(".com")))
            {
                messageBoxText = "Email is ongeldig";
                caption = "FAILED: Email Ongeldig";
                button = MessageBoxButton.OK;
                icon = MessageBoxImage.Error;
            }
            else if (!digits || !special || txtWachtwoord.Password.ToString().Length <= 7)
            {
                messageBoxText = "Wachtwoord moet 8 tekens lang zijn, een cijfer en een speciale teken bevatten";
                caption = "FAILD: Wachtwoord voldoet niet aan de eisen";
                button = MessageBoxButton.OK;
                icon = MessageBoxImage.Error;
            }
            else
            {
                admin = new Admin(txtGebruikersnaam.Text, txtWachtwoord.Password.ToString(), txtEmail.Text);
                messageBoxText = "Admin is aangemaakt";
                caption = "SUCCES";
                button = MessageBoxButton.OK;
                icon = MessageBoxImage.Information;

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "127.0.0.1";
                builder.UserID = "sa";
                builder.Password = "Havermout1325";
                builder.InitialCatalog = "BootVerhuur";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    String sql = "INSERT INTO member " +
                                 "(first_name, last_name, phone_number, email, boating_level, role, username, password)" +
                                 "VALUES ('" + null + "' , '" +
                                 null + "', '" + null + "', '" + txtEmail.Text + "', '" + null + "', '" + "Admin" + "', '" +
                                 txtGebruikersnaam.Text + "', '" + txtWachtwoord.Password + "')";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8}", reader.GetInt32(0),
                                    reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4),
                                    reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8));
                            }
                        }
                    }
                }

                result = MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }

            
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            Close();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

