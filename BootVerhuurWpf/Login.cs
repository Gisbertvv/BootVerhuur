using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BootVerhuur;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Syncfusion.Windows.Shared;
using Windows.Networking;

namespace BootVerhuurWpf
{
    public class Login: Database
    {

        public static string id;
        public static string firstName;
        public static string lastName;
        public static string phoneNumber;
        public static string email;
        public static string boatingLevel;
        public static string role;

        // Login check where password + username match OR password + email match
        public bool getLogin(string usernameOrEmail, string password)
        {
            bool s = false;
            try
            {
             
                using (var connection = GetConnection())
                {
                    connection.Open();
                    String query = "SELECT * FROM member WHERE username=@usernameOrEmail OR email=@usernameOrEmail AND password=@password ";

                    SqlCommand sqlCmd = new SqlCommand(query, connection);
                        

                    //Check if username and password are correct
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@usernameOrEmail", usernameOrEmail);
                    sqlCmd.Parameters.AddWithValue("@password", password);

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
                        connection.Close();
                        s = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return s;
        }
    }
}
