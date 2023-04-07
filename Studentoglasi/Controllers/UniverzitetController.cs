using StudentOglasi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;

namespace StudentOglasi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UniverzitetController : ControllerBase
  {  
        private readonly AppDbContext _dbContext;

        public UniverzitetController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;

        }

    [HttpPost]
    public Univerzitet Snimi([FromBody] UniverzitetVM x)
    {
        Univerzitet? objekat;

        if (x.ID == 0)
        {
            objekat = new Univerzitet();
            _dbContext.Add(objekat);
        }
        else
        {
            objekat = _dbContext.Univerzitet.Find(x.ID);
        }

        objekat.Naziv = x.Naziv;
        objekat.GradID = x.GradID;
            objekat.Email = x.Email;
            objekat.Telefon = x.Telefon;
            objekat.Link = x.Veza;

            _dbContext.SaveChanges(); //exceute sql -- update Predmet set ... where...
        return objekat;
    }

    [HttpGet]
    public ActionResult GetAll()
    {
        var data = _dbContext.Univerzitet
            .OrderBy(s => s.Naziv)
            .Select(s => new
            {
                id = s.ID,
                naziv = s.Naziv,
                email = s.Email,
                telefon = s.Telefon,
                Veza = s.Link,
                grad = s.Grad.Naziv,


            })
            .AsQueryable();
        return Ok(data.Take(100).ToList());
    }
        [HttpGet]
        public ActionResult GetById(int id)
        {
            var univerzitet = _dbContext.Univerzitet.Include(x=>x.Grad)
     .Where(u => u.ID == id)
     .FirstOrDefault();

            if (univerzitet == null)
            {
                return NotFound();
            }

            var objave = _dbContext.ReferentUniverziteta
                .Where(r => r.Univerzitet.ID == id)
                .SelectMany(r => r.Objave)
                .Include(x=>x.Kategorija)
                .ToList();

            var fakulteti = _dbContext.Fakultet
                .Where(f => f.UniverzitetID == id)
                .Select(f => new
                {
                    id=f.ID,
                    naziv=f.Naziv,
                    adresa=f.Adresa,
                    email=f.Email,
                    link=f.Link,
                    logo=f.Logo
                }).ToList();

            var data = new
            {
                id = univerzitet.ID,
                naziv = univerzitet.Naziv, 
                email = univerzitet.Email,
                telefon = univerzitet.Telefon,
                link = univerzitet.Link,
                grad = univerzitet.Grad.Naziv,
                logo=univerzitet.Logo,
                slika=univerzitet.Slika,
                objave = objave.Select(o => new
                {
                    id=o.ID,
                    naslov = o.Naslov,
                    slikaObjave = o.Slika,
                    sadrzaj = o.Sadrzaj,
                    kategorija = o.Kategorija.Naziv,
                    vrijemeObjave=o.VrijemeObjave.ToString("dd.MM.yyyy HH:mm")
                }),
                fakulteti=fakulteti
            };

            return Ok(data);
        }

        [HttpPost("{id}")]
        public ActionResult Obrisi(int id)
        {
            Univerzitet? univerzitet = _dbContext.Univerzitet.Find(id);

            if (univerzitet == null || id == 1)
                return BadRequest("pogresan ID");

            _dbContext.Remove(univerzitet);

            _dbContext.SaveChanges();
            return Ok(univerzitet);
        }
    }
}
