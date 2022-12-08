using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BootVerhuurWpf
{
    public class Admin
    {
        public String Gebruikersnaam { get; set; }
        public String Wachtwoord { get; set; }
        public String Email { get; set; }

        public Admin(String gebruikersnaam, String wachtwoord,String email) 
        { 
            this.Gebruikersnaam = gebruikersnaam;
            this.Wachtwoord = wachtwoord;
            this.Email= email;

            InsertAdmin(connection(), gebruikersnaam, email, wachtwoord);
        }

        public void InsertAdmin(SqlConnection connection, string gebruikersnaam, string email, string wachtwoord)
        {
            String sql = $"INSERT INTO Admins (AdminUserName, Email, Password)" +
                $"VALUES ({gebruikersnaam}, {email}, {wachtwoord});";

            connection.Open();

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                    }
                }
            }
        }
        /// <summary>
        /// Don't know how to make use of this from Bootverhuur
        /// </summary>
        /// <returns></returns>
        public SqlConnection connection()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "<your_server>.database.windows.net";
                builder.UserID = "<your_username>";
                builder.Password = "<your_password>";
                builder.InitialCatalog = "<your_database>";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    return connection;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }
    }
}
