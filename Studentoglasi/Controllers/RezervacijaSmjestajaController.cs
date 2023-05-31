using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentOglasi.Data;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;
using System.Text.Json;
using System.Text.Json.Serialization;

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

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(rezervacija, options);

            _dbContext.SaveChanges(); 
            return Ok(json);
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

        [HttpGet]
        public ActionResult GetByIzdavacId(int izdavacID)
        {
            var data = _dbContext.Rezervacija
                .Where(x=>x.Smjestaj.IzdavacID == izdavacID)
                .GroupBy(x => x.Smjestaj)
                .Select(group => new
                {
                    Smjestaj = group.Key.Naslov, 
                    Rezervacije = group.Select(x => new
                    {
                        student = x.Student.Ime +' '+x.Student.Prezime,
                        brojIndeksa=x.Student.BrojIndeksa,
                        DatumPrijave = x.DatumPrijave,
                        DatumOdjave = x.DatumOdjave,
                        BrojOsoba = x.BrojOsoba
                    })
                })
                .AsQueryable();
            return Ok(data);
        }

        [HttpGet]
        public ActionResult GetByStudentId(int studentID)
        {
            var data = _dbContext.Rezervacija
                .Where(x => x.StudentId == studentID)
                .Select(x => new
                {
                    smjestajId=x.SmjestajId,
                    naslov = x.Smjestaj.Naslov,
                    grad = x.Smjestaj.Grad.Naziv,
                    cijena = x.Smjestaj.Cijena,
                    datumPrijave = x.DatumPrijave,
                    datumOdjave = x.DatumOdjave,
                    brojOsoba = x.BrojOsoba
                })
                .ToList();
            return Ok(data);
        }

        [HttpPost]
        public ActionResult Obrisi(int studentId, int smjestajId)
        {
            Rezervacija rezervacija = _dbContext.Rezervacija.FirstOrDefault(r=>r.StudentId==studentId&&r.SmjestajId==smjestajId);

            if (rezervacija == null)
            {
                return NotFound();
            }

            _dbContext.Remove(rezervacija);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
