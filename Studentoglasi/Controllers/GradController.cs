using StudentOglasi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;

namespace StudentOglasi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GradController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public GradController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;

        }

        [HttpPost]
        public Grad Snimi([FromBody] GradVM x)
        {
            Grad? objekat;

            if (x.ID == 0)
            {
                objekat = new Grad();
                _dbContext.Add(objekat);
            }
            else
            {
                objekat = _dbContext.Grad.Find(x.ID);
            }

            objekat.Naziv = x.Naziv;
            

            _dbContext.SaveChanges(); //exceute sql -- update Predmet set ... where...
            return objekat;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Grad
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
            Grad? grad = _dbContext.Grad.Find(id);

            if (grad == null || id == 1)
                return BadRequest("pogresan ID");

            _dbContext.Remove(grad);

            _dbContext.SaveChanges();
            return Ok(grad);
        }
    }
}
