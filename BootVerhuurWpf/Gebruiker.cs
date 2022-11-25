using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootVerhuurWpf
{
    public interface Gebruiker
    {
        public String Gebruikersnaam { get; set; }
        public String Wachtwoord { get; set; }
        public String Rol { get; set; }
        //public int Id { get; set; }
        public String Email { get; set; }

    }
}
