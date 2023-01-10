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
