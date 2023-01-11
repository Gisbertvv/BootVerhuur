using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using BootVerhuur;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Syncfusion.Windows.Shared;
using Windows.Networking;
using BoldReports.Windows.Data;
using System.Data.SqlTypes;
using Microsoft.IdentityModel.Tokens;

namespace BootVerhuurWpf
{
    public class Login : Database
    {
        public static string id;
        public static string firstName;
        public static string lastName;
        public static string phoneNumber;
        public static string email;
        public static string boatingLevel;
        public static string role;
        public string hashedPassword;

        // Login check where password + username match OR password + email match
        public bool getLogin(string usernameOrEmail, string password)
        {
            bool s = false;

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    String query = "SELECT password FROM member WHERE username=@usernameOrEmail OR email=@usernameOrEmail";

                    SqlCommand sqlCmd = new SqlCommand(query, connection);

                    //Adds value to parameters
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@usernameOrEmail", usernameOrEmail);
                    sqlCmd.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = sqlCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hashedPassword = reader.GetString(0);
                        }
                    }

                    // check if hashed password is not empty and if password matches hashed password
                    if ((hashedPassword != null) && BCrypt.Net.BCrypt.Verify(password, hashedPassword))
                    {
                        String query2 = "SELECT * FROM member WHERE email=@usernameOrEmail AND password=@password OR username=@usernameOrEmail AND password=@password ";
                        SqlCommand sqlCmd2 = new SqlCommand(query2, connection);

                        sqlCmd2.CommandType = System.Data.CommandType.Text;
                        sqlCmd2.Parameters.AddWithValue("@usernameOrEmail", usernameOrEmail);
                        sqlCmd2.Parameters.AddWithValue("@password", hashedPassword);

                        DataTable userTable = new DataTable();
                        userTable.Load(sqlCmd2.ExecuteReader());

                        //Get current user data
                        id = userTable.Rows[0]["id"].ToString();
                        firstName = userTable.Rows[0]["first_name"].ToString();
                        lastName = userTable.Rows[0]["last_name"].ToString();
                        phoneNumber = userTable.Rows[0]["phone_number"].ToString();
                        email = userTable.Rows[0]["email"].ToString();
                        boatingLevel = userTable.Rows[0]["boating_level"].ToString();
                        role = userTable.Rows[0]["role"].ToString();

                        connection.Close();
                        s = true;
                    }
                    else
                    {
                        s = false;
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
