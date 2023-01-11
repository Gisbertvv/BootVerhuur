using System;

namespace BootVerhuurWpf
{
    public class Member
    {
        public static String? Name { get; set; }
        public static String? Surname { get; set; }
        public static String? Username { get; set; }
        public static String? Password { get; set; }
        public static String? Email { get; set; }
        public static String? PhoneNumber { get; set; }
        public static String? Role { get; set; }
        public static String? Level { get; set; }

        public Member(string name, string surname, string username, string password, string email, string phoneNumber, string role, string level)
        {
            Name = name;
            Surname = surname;
            Username = username;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
            Role = role;
            Level = level;

/*            InsertUser(connection(), rol, niveau, voornaam, achternaam, telefoonnummer, email, gebruikersnaam, wachtwoord);      */      
        }

        /*public void InsertUser(SqlConnection connection, string rol, string niveau, string voornaam, string achternaam, string telefoonnummer, string email, string gebruikersnaam, string wachtwoord)
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
        }*/
    }
}
