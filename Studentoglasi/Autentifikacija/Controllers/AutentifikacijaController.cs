﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentOglasi.Autentifikacija.Models;
using StudentOglasi.Autentifikacija.ViewModels;
using StudentOglasi.Data;
using StudentOglasi.Helper;
using StudentOglasi.Helper.AutentifikacijaAutorizacija;
using StudentOglasi.Models;

namespace StudentOglasi.Autentifikacija
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AutentifikacijaController : ControllerBase
    {

        private readonly AppDbContext _dbContext;

        public AutentifikacijaController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;

        }
        [HttpGet("{code}")]
        public ActionResult Otkljucaj(string code)
        {
            var korisnik = HttpContext.GetLoginInfo().korisnik;

            if (korisnik == null)
            {
                return BadRequest("Korisnik nije logiran");
            }

            var token = _dbContext.AutentifikacijaToken.FirstOrDefault(s => s.twoFactorCode == code && s.KorisnikId == korisnik.ID);

            if (token != null)
            {
                token.twoFactorOtkljucano = true;
                _dbContext.SaveChanges();
                FirebaseCloudMessaging.SendNotification("Prijava ", "Uspješno ste se prijavili", "success");
                return Ok();
            }
            return BadRequest("pogresan URL");

        }


        [HttpPost]
        public ActionResult<LoginInformacije> Login([FromBody] LoginVM x)
        {
            //1- provjera logina
            Korisnik? logiraniKorisnik = _dbContext.Korisnik
                .FirstOrDefault(k =>
                k.Username != null && k.Username == x.username && k.Password == x.password);

            if (logiraniKorisnik == null)
            {
                //pogresan username i password
                return new LoginInformacije(null);
            }

            //2- generisati random string
            string randomString = TokenGenerator.Generate(10);
            string twoFactorCode = TokenGenerator.Generate(4);

            //3- dodati novi zapis u tabelu AutentifikacijaToken za logiraniKorisnikId i randomString
            var noviToken = new AutentifikacijaToken()
            {
                vrijednost = randomString,
                korisnik = logiraniKorisnik,
                vrijemeEvidentiranja = DateTime.Now,
                twoFactorCode=twoFactorCode,

            };

            _dbContext.Add(noviToken);
            _dbContext.SaveChanges();

            EmailLog.uspjesnoLogiranKorisnik(noviToken, Request.HttpContext);

            //4- vratiti token string
            return new LoginInformacije(noviToken);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            AutentifikacijaToken autentifikacijaToken = HttpContext.GetAuthToken();

            if (autentifikacijaToken == null)
                return Ok();

            _dbContext.Remove(autentifikacijaToken);
            _dbContext.SaveChanges();
            FirebaseCloudMessaging.SendNotification("Odjava ", "Uspješno ste se odjavili", "success");
            return Ok();
        }

        [HttpGet]
        public ActionResult<AutentifikacijaToken> Get()
        {
            AutentifikacijaToken autentifikacijaToken = HttpContext.GetAuthToken();

            return autentifikacijaToken;
        }
    }
}
