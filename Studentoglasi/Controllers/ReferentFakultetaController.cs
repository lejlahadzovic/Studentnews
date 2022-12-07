using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentOglasi.Data;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;

namespace StudentOglasi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReferentFakultetaController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ReferentFakultetaController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public ActionResult Snimi([FromBody] ReferentFakultetaVM x)
        {
            ReferentFakulteta referent;
            if (x.id == 0)
            {
                referent = new ReferentFakulteta();
                _dbContext.Add(referent);
            }
            else
            {
                referent = _dbContext.ReferentFakulteta.Find(x.id);
            }
            referent.Username = x.username;
            referent.Password = x.password;
            referent.Ime = x.ime;
            referent.Prezime = x.prezime;
            referent.Email = x.email;
            referent.FakultetID = x.fakultetID;

            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult<List<ReferentFakultetaVM>> GetAll()
        {
            var data = _dbContext.ReferentFakulteta.
                OrderBy(x => x.Ime)
                .Select(x => new ReferentFakultetaVM
                {
                    id = x.ID,
                    password = x.Password,
                    username = x.Username,
                    ime = x.Ime,
                    prezime = x.Prezime,
                    email = x.Email,
                    fakultetID = x.FakultetID,
                    naziv_fakulteta = x.Fakultet.Naziv
                });
            return data.ToList();
        }

        [HttpPost]
        public ActionResult Obrisi([FromBody] ReferentFakultetaVM referent)
        {
            ReferentFakulteta r = _dbContext.ReferentFakulteta.Find(referent.id);
            _dbContext.Remove(r);
            _dbContext.SaveChanges();
            return Ok();
        }

    }
}
