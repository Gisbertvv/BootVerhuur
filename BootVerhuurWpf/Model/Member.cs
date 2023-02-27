using System;

namespace BootVerhuurWpf.Model
{
    public class Members
    {
        public static string? Name { get; set; }
        public static string? Surname { get; set; }
        public static string? Username { get; set; }
        public static string? Password { get; set; }
        public static string? Email { get; set; }
        public static string? PhoneNumber { get; set; }
        public static string? Role { get; set; }
        public static string? Level { get; set; }

        public Members(string name, string surname, string username, string password, string email, string phoneNumber, string role, string level)
        {
            Name = name;
            Surname = surname;
            Username = username;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
            Role = role;
            Level = level;

        }
    }
}
