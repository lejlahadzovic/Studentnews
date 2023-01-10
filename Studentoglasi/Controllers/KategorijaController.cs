using StudentOglasi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;


namespace StudentOglasi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KategorijaController : Controller
    {



        private readonly AppDbContext _dbContext;

        public KategorijaController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;

        }

        [HttpPost]
        public Kategorija Snimi([FromBody] KategorijaVM x)
        {
            Kategorija? objekat;

            if (x.ID == 0)
            {
                objekat = new Kategorija();
                _dbContext.Add(objekat);
            }
            else
            {
                objekat = _dbContext.Kategorija.Find(x.ID);
            }

            objekat.Naziv = x.Naziv;
          
            _dbContext.SaveChanges(); //exceute sql -- update Predmet set ... where...
            return objekat;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Kategorija
                .OrderBy(s => s.Naziv)
                .Select(s => new
                {
                    id = s.ID,
                    naziv = s.Naziv,
                


                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }

        [HttpPost("{id}")]
        public ActionResult Obrisi(int id)
        {
            Kategorija? kategorija = _dbContext.Kategorija.Find(id);

            if (kategorija == null || id == 1)
                return BadRequest("pogresan ID");

            _dbContext.Remove(kategorija);

            _dbContext.SaveChanges();
            return Ok(kategorija);
        }
    }
}

