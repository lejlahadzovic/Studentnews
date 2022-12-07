using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentOglasi.Data;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;

namespace StudentOglasi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UposlenikFirmeController : ControllerBase
    {

        private readonly AppDbContext _dbContext;

        public UposlenikFirmeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult Snimi([FromBody] UposlenikFirmeVM x)
        {
            UposlenikFirme uposlenik;
            if (x.id == 0)
            {
                uposlenik = new UposlenikFirme();
                _dbContext.Add(uposlenik);
            }
            else
            {
                uposlenik = _dbContext.UposlenikFirme.Find(x.id);
            }
            uposlenik.Username = x.username;
            uposlenik.Password = x.password;
            uposlenik.Ime = x.ime;
            uposlenik.Prezime = x.prezime;
            uposlenik.Email = x.email;
            uposlenik.FirmaID = x.firmaID;
            uposlenik.Pozicija = x.pozicija;

            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult<List<UposlenikFirmeVM>> GetAll()
        {
            var data = _dbContext.UposlenikFirme.
                OrderBy(u => u.Ime)
                .Select(u => new UposlenikFirmeVM
                {
                    id = u.ID,
                    password = u.Password,
                    username = u.Username,
                    ime = u.Ime,
                    prezime = u.Prezime,
                    email = u.Email,
                    firmaID = u.FirmaID,
                    naziv_firme = u.Firma.Naziv,
                    pozicija=u.Pozicija
                });
            return data.ToList();
        }

        [HttpPost]
        public ActionResult Obrisi([FromBody] UposlenikFirmeVM uposlenik)
        {
            UposlenikFirme u = _dbContext.UposlenikFirme.Find(uposlenik.id);
            _dbContext.Remove(u);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
