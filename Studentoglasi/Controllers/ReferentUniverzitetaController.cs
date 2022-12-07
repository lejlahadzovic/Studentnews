using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentOglasi.Data;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;

namespace StudentOglasi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReferentUniverzitetaController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ReferentUniverzitetaController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public ActionResult Snimi([FromBody] ReferentUniverzitetaVM x)
        {
            ReferentUniverziteta referent;
            if (x.id == 0)
            {
                referent = new ReferentUniverziteta();
                _dbContext.Add(referent);
            }
            else
            {
                referent = _dbContext.ReferentUniverziteta.Find(x.id);
            }
            referent.Username = x.username;
            referent.Password = x.password;
            referent.Ime = x.ime;
            referent.Prezime = x.prezime;
            referent.Email = x.email;
            referent.UnivetzitetID = x.univerzitetID;

            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult<List<ReferentUniverzitetaVM>> GetAll()
        {
            var data = _dbContext.ReferentUniverziteta.
                OrderBy(x => x.Ime)
                .Select(x => new ReferentUniverzitetaVM
                {
                    id = x.ID,
                    password = x.Password,
                    username = x.Username,
                    ime = x.Ime,
                    prezime = x.Prezime,
                    email = x.Email,
                    univerzitetID = x.UnivetzitetID,
                    naziv_univerziteta = x.Univerzitet.Naziv
                });
            return data.ToList();
        }

        [HttpPost]
        public ActionResult Obrisi([FromBody] ReferentUniverzitetaVM referent)
        {
            ReferentUniverziteta r = _dbContext.ReferentUniverziteta.Find(referent.id);
            _dbContext.Remove(r);
            _dbContext.SaveChanges();
            return Ok();
        }

    }
}
