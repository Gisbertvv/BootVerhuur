using System;
using System.Windows;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Media;
using Syncfusion.Pdf.Parsing;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public static string id;
        public static string firstName;
        public static string lastName;
        public static string phoneNumber;
        public static string email;
        public static string boatingLevel;
        public static string role;

        public Login()
        {
            AdminPanel panel = new AdminPanel();
            InitializeComponent();
    
            Color color = (Color)ColorConverter.ConvertFromString(panel.GetColors()[2]);
            SolidColorBrush solidColorBrush = new SolidColorBrush(color);
            gridje.Background = solidColorBrush;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //Connect to database
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "127.0.0.1";
            builder.UserID = "sa";
            builder.Password = "Havermout1325";
            builder.InitialCatalog = "BootVerhuur";
            SqlConnection connection = new SqlConnection(builder.ConnectionString);

            try
            {
                // Check if connection is closed
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                    String query = "SELECT * FROM member WHERE username=@username AND password=@password";
                    SqlCommand sqlCmd = new SqlCommand(query, connection);

                    //Check if username and password are correct
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    sqlCmd.Parameters.AddWithValue("@password", txtPassword.Password);

                    //DataTable dataTable = new DataTable();
                    DataTable userTable = new DataTable();

                    userTable.Load(sqlCmd.ExecuteReader());

                    if (userTable.Rows.Count == 1)
                    {
                        id = userTable.Rows[0]["id"].ToString();
                        firstName = userTable.Rows[0]["first_name"].ToString();
                        lastName = userTable.Rows[0]["last_name"].ToString();
                        phoneNumber = userTable.Rows[0]["phone_number"].ToString();
                        email = userTable.Rows[0]["email"].ToString();
                        boatingLevel = userTable.Rows[0]["boating_level"].ToString();
                        role = userTable.Rows[0]["role"].ToString();

                        //id = dataTable.Columns.Add("id").ColumnName;
                        //id = dataTable.Columns.Contains();

                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    else
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
