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

        public Admin(String gebruikersnaam, String wachtwoord, String email)
        {
            this.Gebruikersnaam = gebruikersnaam;
            this.Wachtwoord = wachtwoord;
            this.Email = email;

/*            InsertAdmin(connection(), gebruikersnaam, email, wachtwoord);*/
        }
    }
}
