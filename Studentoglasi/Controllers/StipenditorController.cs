using StudentOglasi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;

namespace StudentOglasi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StipenditorController : Controller
    {



        private readonly AppDbContext _dbContext;

        public StipenditorController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;

        }

        [HttpPost]
        public Stipenditor Snimi([FromBody] StipenditorVM x)
        {
            Stipenditor? objekat;

            if (x.ID == 0)
            {
                objekat = new Stipenditor();
                _dbContext.Add(objekat);
            }
            else
            {
                objekat = _dbContext.Stipenditor.Find(x.ID);
            }

            objekat.Naziv = x.Naziv;
            objekat.Adresa = x.Adresa;
            objekat.Email = x.Email;
            objekat.TipUstanove = x.TipUstanove;
            objekat.Link = x.Veza;
            objekat.GradID = x.GradID;
          //  objekat.Ocjene = x.Ocjene;
            _dbContext.SaveChanges(); //exceute sql -- update Predmet set ... where...
            return objekat;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Stipenditor
                .OrderBy(s => s.Naziv)
                .Select(s => new
                {
                    id = s.ID,
                    naziv = s.Naziv,
                    adresa = s.Adresa,
                    email = s.Email,
                    tipUstanove = s.TipUstanove,
                    veza = s.Link,
                    grad = s.Grad.Naziv,
                 //   ocjene = s.Ocjene,


                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }

        [HttpPost("{id}")]
        public ActionResult Obrisi(int id)
        {
            Stipenditor? stipenditor = _dbContext.Stipenditor.Find(id);

            if (stipenditor == null || id == 1)
                return BadRequest("pogresan ID");

            _dbContext.Remove(stipenditor);

            _dbContext.SaveChanges();
            return Ok(stipenditor);
        }
    }
}
