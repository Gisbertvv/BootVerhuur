using System;
using System.Windows;
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
            //Connect to database
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.UserID = "SA";
            builder.Password = "Havermout1325";
            builder.InitialCatalog = "BootVerhuur";
            SqlConnection connection = new SqlConnection(builder.ConnectionString);

            try
            {
                // Check if connection is closed
                if(connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                    String query = "SELECT COUNT(1) FROM member WHERE username=@username AND password=@password";
                    SqlCommand sqlCmd = new SqlCommand(query, connection);

                    //Check if username and password are correct
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
