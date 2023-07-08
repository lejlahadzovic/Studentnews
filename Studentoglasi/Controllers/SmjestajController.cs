using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentOglasi.Data;
using StudentOglasi.Helper;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace StudentOglasi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SmjestajController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;

        public SmjestajController(AppDbContext dbContext, IHostingEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public ActionResult Snimi([FromForm] SmjestajVM x)
        {
            try
            {
                bool edit = false;
                Smjestaj smjestaj = new Smjestaj();

                if (x.id == 0)
                {
                    smjestaj = new Smjestaj();
                    _dbContext.Add(smjestaj);
                    smjestaj.VrijemeObjave = DateTime.Now;
                }
                else
                {
                    smjestaj = _dbContext.Smjestaj.Find(x.id);
                    edit = true;
                }

                smjestaj.RokPrijave = x.rokPrijave;
                smjestaj.Naslov = x.naslov;
                smjestaj.Opis = x.opis;
                smjestaj.Cijena = x.cijena;
                smjestaj.Kapacitet = x.kapacitet;
                smjestaj.DodatneUsluge = x.dodatneUsluge;
                smjestaj.BrojSoba = x.brojSoba;
                smjestaj.Parking = x.parking;
                smjestaj.NacinGrijanja = x.nacinGrijanja;
                smjestaj.GradID = x.gradID;
                smjestaj.IzdavacID = x.izdavacID;
                smjestaj.Slika = DodajFile.UploadSlike(x.slika, smjestaj.Slika, _hostingEnvironment);

                _dbContext.SaveChanges();
                return Ok(smjestaj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }

        [HttpGet]
        public ActionResult GetById(int id)
        {
            var data = _dbContext.Smjestaj
                .Include(x=>x.Ocjene)
                .Where(x => x.ID == id)
                .OrderBy(x => x.ID)
                .Select(x => new
                {
                    id = x.ID,
                    naslov = x.Naslov,
                    rokPrijave = x.RokPrijave,
                    opis = x.Opis,
                    slika = Config.SlikePutanja + x.Slika,
                    vrijemeObjave = x.VrijemeObjave.ToString("dd.MM.yyyy H:mm"),
                    izdavac = x.Izdavac.Ime + ' ' + x.Izdavac.Prezime,
                    izdavacSlika= x.Izdavac.Slika,
                    nacinGrijanja = x.NacinGrijanja,
                    cijena = x.Cijena,
                    kapacitet = x.Kapacitet,
                    dodatneUsluge = x.DodatneUsluge,
                    brojSoba = x.BrojSoba,
                    parking = x.Parking,
                    grad_naziv = x.Grad.Naziv,
                    prosjecnaOcjena = x.Ocjene.Any() ? Math.Round(x.Ocjene.Average(o => o.Vrijednost),2) : 0
                })
                .FirstOrDefault();

            if (data == null)
            {
                return BadRequest("Smještaj ne postoji");
            }
            return Ok(data);
        }

        [HttpGet]
        public ActionResult Get(int? izdavacID, int? pageNumber, int pageSize = 10)
        {
            var data = _dbContext.Smjestaj
                .Where(x => izdavacID == null || x.IzdavacID == izdavacID)
                .OrderBy(x => x.ID)
                .Select(x => new
                {
                    id = x.ID,
                    naslov = x.Naslov,
                    rokPrijave = x.RokPrijave,
                    opis = x.Opis,
                    slika = Config.SlikePutanja + x.Slika,
                    vrijemeObjave = x.VrijemeObjave.ToString("dd.MM.yyyy H:mm"),
                    izdavacID = x.IzdavacID,
                    izdavac = x.Izdavac.Ime + ' ' + x.Izdavac.Prezime,
                    nacinGrijanja=x.NacinGrijanja,
                    cijena=x.Cijena,
                    kapacitet=x.Kapacitet,
                    dodatneUsluge=x.DodatneUsluge,
                    brojSoba=x.BrojSoba,
                    parking=x.Parking,
                    gradID=x.GradID,
                    grad_naziv=x.Grad.Naziv
                })
                .AsQueryable();

            if (izdavacID != null)
                return Ok(data);

            else if (pageNumber != null)
            {
                var pagedList = PagedList<object>.Create(data, pageNumber.Value, pageSize);
                return Ok(pagedList);
            }
            return Ok(data);
        }

        [HttpPost]
        public ActionResult Obrisi([FromBody] int id)
        {
            Smjestaj x = _dbContext.Smjestaj.Find(id);
            DodajFile.ObrisiSliku(x.Slika, _hostingEnvironment);

            _dbContext.Remove(x);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public ActionResult OcijeniSmjestaj([FromBody] OcjenaVM x)
        {
            var smjestaj = _dbContext.Smjestaj
                .Include(s=>s.Ocjene)
                .FirstOrDefault(s => s.ID == x.smjestajId);
            var student = _dbContext.Student.FirstOrDefault(s => s.ID == x.studentId);

            if (smjestaj == null)
                return BadRequest("Smještaj ne postoji");
            if (student == null)
                return BadRequest("Pograsan ID studenta");

            if (x.ocjena < 1 || x.ocjena > 5)
                return BadRequest("Ocjena mora biti između 1 i 5");

            var ocjena = smjestaj.Ocjene?.FirstOrDefault(o => o.StudentId == x.studentId);

            if (ocjena != null)
            {
                ocjena.Vrijednost = x.ocjena;
                ocjena.Komentar = x.komentar;
            }
            else
            {
                ocjena = new Ocjena
                {
                    StudentId = x.studentId,
                    Vrijednost = x.ocjena,
                    Komentar = x.komentar
                };
                smjestaj.Ocjene.Add(ocjena);
            }
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
