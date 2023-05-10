using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentOglasi.Data;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;

namespace StudentOglasi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RezervacijaSmjestajaController : Controller

    {     
        private readonly AppDbContext _dbContext;
    public RezervacijaSmjestajaController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
       
        [HttpPost]
        public ActionResult Snimi([FromBody] RezervacijaVM x)
        {
            Rezervacija rezervacija;

            if (x.ID == 0)
            {
                rezervacija = new Rezervacija();
                _dbContext.Add(rezervacija);
            }
            else
            {
                rezervacija = _dbContext.Rezervacija.Find(x.ID);
            }

            rezervacija.SmjestajId = x.SmjestajId;
            rezervacija.Smjestaj = _dbContext.Smjestaj.Find(x.SmjestajId);
            rezervacija.StudentId = x.StudentId;
            rezervacija.Student = _dbContext.Student.Find(x.StudentId);
            rezervacija.DatumPrijave = x.DatumPrijave;
            rezervacija.DatumOdjave = x.DatumOdjave;
            rezervacija.BrojOsoba = x.BrojOsoba;
        
            _dbContext.SaveChanges(); 
            return Ok(rezervacija);
        }


     
        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Rezervacija
                .OrderBy(x => x.StudentId)
                .Select(x => new
                {
                    SmjestajID=x.SmjestajId,
                    studentIme = x.Student.Ime,
                    studentID = x.StudentId,
                    DatumPrijave=x.DatumPrijave,
                    DatumOdjave=x.DatumOdjave,
                    BrojOsoba=x.BrojOsoba,
                })
                .AsQueryable();
            return Ok(data);
        }
    }
}
