using System;
namespace BootVerhuurWpf
{
    public class Admin
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }

        public Admin(String username, String password, String email)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
        }
    }
}
