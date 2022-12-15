using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BootVerhuurWpf
{
    public class Member
    {
        public static String? Voornaam { get; set; }
        public static String? Achternaam { get; set; }
        public static String? Gebruikersnaam { get; set; }
        public static String? Wachtwoord { get; set; }
        public static String? Email { get; set; }
        public static String? Telefoonnummer { get; set; }
        public static String? Rol { get; set; }
        public static String? Niveau { get; set; }

        public Member(string voornaam, string achternaam, string gebruikersnaam, string wachtwoord, string email, string telefoonnummer, string rol, string niveau)
        {
            Voornaam = voornaam;
            Achternaam = achternaam;
            Gebruikersnaam = gebruikersnaam;
            Wachtwoord = wachtwoord;
            Email = email;
            Telefoonnummer = telefoonnummer;
            Rol = rol;
            Niveau = niveau;

/*            InsertUser(connection(), rol, niveau, voornaam, achternaam, telefoonnummer, email, gebruikersnaam, wachtwoord);      */      
        }

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

        public void InsertUser(SqlConnection connection, string rol, string niveau, string voornaam, string achternaam, string telefoonnummer, string email, string gebruikersnaam, string wachtwoord)
        {
            String sql = $"INSERT INTO Users (Rol, Niveau, Voornaam, Achternaam, Telefoonnummer, Email, Gebruikersnaam, Wachtwoord)" +
                $"VALUES ({rol}, {niveau}, {voornaam}, {achternaam}, {telefoonnummer}, {email}, {gebruikersnaam}, {wachtwoord});";

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
    }
}
