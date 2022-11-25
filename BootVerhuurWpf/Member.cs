using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootVerhuurWpf
{
    public class Member
    {
        public String Voornaam { get; set; }
        public String Achternaam { get; set; }
        public String Gebruikersnaam { get; set; }
        public String Wachtwoord { get; set; }
        public String Email { get; set; }
        public String Telefoonnummer { get; set; }
        public String Rol { get; set; }
        public String Niveau { get; set; }

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
        }
    }
}
