using StudentOglasi.Autentifikacija.Models;
using StudentOglasi.Models;
using System.Text.Json.Serialization;

namespace StudentOglasi.Helper.AutentifikacijaAutorizacija
{
    public class LoginInformacije
    {
        public LoginInformacije(AutentifikacijaToken autentifikacijaToken)
        {
            this.autentifikacijaToken = autentifikacijaToken;
        }


        [JsonIgnore]
        public Korisnik korisnickiNalog => autentifikacijaToken?.korisnik;
        public AutentifikacijaToken autentifikacijaToken { get; set; }
        public bool isLogiran => korisnickiNalog != null;
    }
}
