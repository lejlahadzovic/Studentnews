using StudentOglasi.Autentifikacija.Models;
using Microsoft.AspNetCore.Http;
using StudentOglasi.Data;
using Microsoft.EntityFrameworkCore;

namespace StudentOglasi.Helper.AutentifikacijaAutorizacija
{
    public static class AuthToken
    {
        public static AutentifikacijaToken GetAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.GetMyAuthToken();
            AppDbContext? db = httpContext.RequestServices.GetService<AppDbContext>();

            AutentifikacijaToken? korisnik = db.AutentifikacijaToken
                .Include(s => s.korisnik)
                .SingleOrDefault(x => token != null && x.vrijednost == token);

            return korisnik;
        }
        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string? token = httpContext.Request.Headers["autentifikacija-token"];
            return token;
        }
    }
}
