using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentOglasi.Data;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;

namespace StudentOglasi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class IzdavacSmjestajaController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public IzdavacSmjestajaController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult Snimi([FromBody] IzdavacSmjestajaVM x)
        {
            IzdavacSmjestaja izdavac;
            if (x.id == 0)
            {
                izdavac = new IzdavacSmjestaja();
                _dbContext.Add(izdavac);
            }
            else
            {
                izdavac = _dbContext.IzdavacSmjestaja.Find(x.id);
            }
            izdavac.Username = x.username;
            izdavac.Password = x.password;
            izdavac.Ime = x.ime;
            izdavac.Prezime = x.prezime;
            izdavac.Telefon = x.broj_telefona;
            izdavac.Email = x.email;

            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult<List<IzdavacSmjestajaVM>> GetAll()
        {
            var data = _dbContext.IzdavacSmjestaja.
                OrderBy(x => x.Ime)
                .Select(x => new IzdavacSmjestajaVM
                {
                    id = x.ID,
                    password = x.Password,
                    username = x.Username,
                    ime = x.Ime,
                    prezime = x.Prezime,
                    broj_telefona = x.Telefon,
                    email = x.Email
                });
            return data.ToList();
        }

        [HttpPost]
        public ActionResult Obrisi([FromBody] IzdavacSmjestajaVM x)
        {
            IzdavacSmjestaja izdavac = _dbContext.IzdavacSmjestaja.Find(x.id);
            _dbContext.Remove(izdavac);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
