using StudentOglasi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;


namespace StudentOglasi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FirmaController : Controller
    {

        private readonly AppDbContext _dbContext;

        public FirmaController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;

        }

        [HttpPost]
        public Firma Snimi([FromBody] FirmaVM x)
        {
            Firma? objekat;

            if (x.ID == 0)
            {
                objekat = new Firma();
                _dbContext.Add(objekat);
            }
            else
            {
                objekat = _dbContext.Firma.Find(x.ID);
            }

            objekat.Naziv = x.Naziv;
            objekat.Adresa = x.Adresa;
            objekat.Email = x.Email;
            objekat.Telefon = x.Telefon;
            objekat.Link = x.Veza;
            objekat.GradID = x.GradID;
           // objekat.Ocjene = x.Ocjene;
            _dbContext.SaveChanges(); //exceute sql -- update Predmet set ... where...
            return objekat;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Firma
                .OrderBy(s => s.Naziv)
                .Select(s => new
                {
                    id = s.ID,
                    naziv = s.Naziv,
                    adresa = s.Adresa,
                    email = s.Email,
                    telefon = s.Telefon,
                    Veza = s.Link,
                    grad = s.Grad.Naziv,
                   // ocjene = s.Ocjene,


                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }

        [HttpPost("{id}")]
        public ActionResult Obrisi(int id)
        {
            Firma? firma = _dbContext.Firma.Find(id);

            if (firma == null || id == 1)
                return BadRequest("pogresan ID");

            _dbContext.Remove(firma);

            _dbContext.SaveChanges();
            return Ok(firma);
        }
    }
}

