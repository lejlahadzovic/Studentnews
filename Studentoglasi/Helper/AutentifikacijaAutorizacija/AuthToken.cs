using StudentOglasi.Autentifikacija.Models;
using Microsoft.AspNetCore.Http;
using StudentOglasi.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using StudentOglasi.Models;

namespace StudentOglasi.Helper.AutentifikacijaAutorizacija
{
    public static class AuthToken
    {
        public class LoginInformacije
        {
            public LoginInformacije(AutentifikacijaToken? autentifikacijaToken)
            {
                this.autentifikacijaToken = autentifikacijaToken;
            }

            [JsonIgnore]
            public Korisnik? korisnik => autentifikacijaToken?.korisnik;
            public AutentifikacijaToken? autentifikacijaToken { get; set; }

            public bool isLogiran => korisnik != null;

        } 
        public static LoginInformacije GetLoginInfo(this HttpContext httpContext)
        {
            var token = httpContext.GetAuthToken();

            return new LoginInformacije(token);
        }
        public static AutentifikacijaToken GetAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.GetMyAuthToken();
            AppDbContext? db = httpContext.RequestServices.GetService<AppDbContext>();

            AutentifikacijaToken? korisnik = db?.AutentifikacijaToken
                .Include(s => s.korisnik)
                .SingleOrDefault( x=>x.vrijednost == token);

            return korisnik;
        }

    
        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["autentifikacija-token"];
            return token;
        }
    }
}
