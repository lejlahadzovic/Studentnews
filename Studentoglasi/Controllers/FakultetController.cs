using StudentOglasi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;
using StudentOglasi.Helper;

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
               // objekat.Ocjene=x.Ocjene;
            _dbContext.SaveChanges(); //exceute sql -- update Predmet set ... where...
                return objekat;
            }

            [HttpGet]
            public ActionResult GetAll(int? pageNumber, int pageSize = 10)
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
                        //ocjene=s.Ocjene,


                    })
                    .AsQueryable();
            if (pageNumber != null)
            {
                var pagedList = PagedList<object>.Create(data, pageNumber.Value, pageSize);
                return Ok(pagedList);
            }
            return Ok(data);
        }
        [HttpGet]
        public ActionResult GetById(int id, int objavePageNumber=1)
        {
            var fakultet = _dbContext.Fakultet
                .Include(x => x.Univerzitet)
                .Include(x=>x.Univerzitet.Grad)
                .Include(x=>x.Ocjene)
                .Where(x => x.ID == id)
                .FirstOrDefault();

            if (fakultet == null)
                return BadRequest("Fakultet ne postoji");

            var objaveQuery = _dbContext.ReferentFakulteta
                .Where(r => r.Fakultet.ID == id)
                .SelectMany(r => r.Objave)
                .Include(x => x.Kategorija)
                .OrderByDescending(x=>x.VrijemeObjave);

            var pagedObjave = PagedList<Objava>.Create(objaveQuery, objavePageNumber, 4);

            var objave = PagedList<Objava>.Create(objaveQuery, objavePageNumber, 4)
                .DataItems
                .Select(o => new
                {
                    id = o.ID,
                    naslov = o.Naslov,
                    slikaObjave = o.Slika,
                    sadrzaj = o.Sadrzaj,
                    kategorija = o.Kategorija.Naziv,
                    vrijemeObjave = o.VrijemeObjave.ToString("dd.MM.yyyy HH:mm")
                });

            var data = new
            {
                id = fakultet.ID,
                naziv = fakultet.Naziv,
                email = fakultet.Email,
                telefon = fakultet.Telefon,
                link = fakultet.Link,
                grad = fakultet.Univerzitet.Grad.Naziv,
                univerzitet=fakultet.Univerzitet.Naziv,
                logo = fakultet.Logo,
                slika = fakultet.Slika,
                opis=fakultet.Opis,
                objave = objave,
                prosjecnaOcjena= fakultet.Ocjene.Any() ? Math.Round(fakultet.Ocjene.Average(o => o.Vrijednost),2) : 0,
                objaveTotalCount = pagedObjave.TotalCount,
                objavePageSize = pagedObjave.PageSize
            };

            return Ok(data);
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

        [HttpPost]
        public ActionResult OcijeniFakultet([FromBody] OcjenaVM x)
        {
            var fakultet = _dbContext.Fakultet
                .Include(f=>f.Ocjene)
                .FirstOrDefault(f => f.ID == x.fakultetId);
            var student = _dbContext.Student.FirstOrDefault(s => s.ID == x.studentId);

            if (fakultet == null)
                return BadRequest("Fakultet ne postoji");
            if (student == null)
                return BadRequest("Pograsan ID studenta");

            if (x.ocjena < 1 || x.ocjena > 5)
                return BadRequest("Ocjena mora biti između 1 i 5");

            var ocjena = fakultet.Ocjene?.FirstOrDefault(o=>o.StudentId==x.studentId);

            if(ocjena != null)
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
                    Komentar=x.komentar
                };
                fakultet.Ocjene.Add(ocjena);
            }
            _dbContext.SaveChanges();
            FirebaseCloudMessaging.SendNotification("Ocjena", "Vaša ocjena je uspješno zabilježena", "success");
            return Ok();
        }

        }
    }

