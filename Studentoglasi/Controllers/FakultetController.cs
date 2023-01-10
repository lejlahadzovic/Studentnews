using StudentOglasi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;


namespace StudentOglasi.Controllers
{ 
        [ApiController]
        [Route("[controller]/[action]")]
    public class FakultetController : ControllerBase
    {

        private readonly AppDbContext _dbContext;

        public FakultetController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;

        }

        [HttpPost]
            public Fakultet Snimi([FromBody] FakultetVM x)
            {
                Fakultet? objekat;

                if (x.ID == 0)
                {
                    objekat = new Fakultet();
                    _dbContext.Add(objekat);
                }
                else
                {
                    objekat = _dbContext.Fakultet.Find(x.ID);
                }

                objekat.Naziv = x.Naziv;
                objekat.Adresa = x.Adresa;
                objekat.Email = x.Email;
                objekat.Telefon = x.Telefon;
                objekat.Link = x.Veza;
                objekat.UniverzitetID = x.UniverzitetID;
                objekat.Ocjene=x.Ocjene;
            _dbContext.SaveChanges(); //exceute sql -- update Predmet set ... where...
                return objekat;
            }

            [HttpGet]
            public ActionResult GetAll()
            {
                var data = _dbContext.Fakultet
                    .OrderBy(s => s.Naziv)
                    .Select(s => new
                    {
                        id = s.ID,
                        naziv = s.Naziv,
                        adresa = s.Adresa,
                        email = s.Email,
                        telefon = s.Telefon,
                        Veza = s.Link,
                        univerzitet = s.Univerzitet.Naziv,
                        ocjene=s.Ocjene,


                    })
                    .AsQueryable();
                return Ok(data.Take(100).ToList());
            }

            [HttpPost("{id}")]
            public ActionResult Obrisi(int id)
            {
                Fakultet? fakultet = _dbContext.Fakultet.Find(id);

                if (fakultet == null || id == 1)
                    return BadRequest("pogresan ID");

                _dbContext.Remove(fakultet);

                _dbContext.SaveChanges();
                return Ok(fakultet);
            }
        }
    }

