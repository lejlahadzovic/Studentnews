using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentOglasi.Data;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;

namespace StudentOglasi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UposlenikStipenditoraController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public UposlenikStipenditoraController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult Snimi([FromBody] UposlenikStipenditoraVM x)
        {
            UposlenikStipenditora uposlenik;
            if (x.id == 0)
            {
                uposlenik = new UposlenikStipenditora();
                _dbContext.Add(uposlenik);
            }
            else
            {
                uposlenik = _dbContext.UposlenikStipenditora.Find(x.id);
            }
            uposlenik.Username = x.username;
            uposlenik.Password = x.password;
            uposlenik.Ime = x.ime;
            uposlenik.Prezime = x.prezime;
            uposlenik.Email = x.email;
            uposlenik.StipenditorID = x.stipenditorID;

            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult<List<UposlenikStipenditoraVM>> GetAll()
        {
            var data = _dbContext.UposlenikStipenditora.
                OrderBy(u => u.Ime)
                .Select(u => new UposlenikStipenditoraVM
                {
                    id = u.ID,
                    password = u.Password,
                    username = u.Username,
                    ime = u.Ime,
                    prezime = u.Prezime,
                    email = u.Email,
                    stipenditorID = u.StipenditorID,
                    naziv_stipenditora = u.Stipenditor.Naziv
                });
            return data.ToList();
        }

        [HttpPost]
        public ActionResult Obrisi([FromBody] UposlenikStipenditoraVM uposlenik)
        {
            UposlenikStipenditora u = _dbContext.UposlenikStipenditora.Find(uposlenik.id);
            _dbContext.Remove(u);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
