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
using System.Data.SqlClient;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for LoginScreenWindow.xaml
    /// </summary>
    public partial class LoginScreenWindow : Window
    {
        public LoginScreenWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.UserID = "SA";
            builder.Password = "Havermout1325";
            builder.InitialCatalog = "BootVerhuur";
            SqlConnection connection = new SqlConnection(builder.ConnectionString);

            try
            {
                if(connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                    String query = "SELECT COUNT(1) FROM member WHERE username=@username AND password=@password";
                    SqlCommand sqlCmd = new SqlCommand(query, connection);
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    sqlCmd.Parameters.AddWithValue("@password", txtPassword.Password);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

                    if (count == 1)
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    } else
                    {
                        MessageBox.Show("Usename of password not correct");
                    }
                }

                //{
                //    String sql = "SELECT * FROM member";
                //    using (SqlCommand command = new SqlCommand(sql, connection))
                //    {
                //        connection.Open();
                //        using (SqlDataReader reader = command.ExecuteReader())
                //        {
                //            while (reader.Read())
                //            {
                //                Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                //            }
                //        }
                //    }

                //}
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
