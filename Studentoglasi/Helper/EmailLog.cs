using FIT_Api_Examples.Helper;
using StudentOglasi.Autentifikacija.Models;

namespace StudentOglasi.Helper
{
    public class EmailLog
    {
        public static void uspjesnoLogiranKorisnik(AutentifikacijaToken token, HttpContext httpContext)
        {
            var logiranKorisnik = token.korisnik;
            
            var poruka= $"Poštovani, {logiranKorisnik.Username}, <br>"+ $"Kod za autentifikaciju je <br>"+$" {token.twoFactorCode} "+$" Login info {DateTime.Now}";
            EmailSender.Posalji(logiranKorisnik.Email, "Kod za autentifikaciju", poruka,true);
            
        }

    }
}
