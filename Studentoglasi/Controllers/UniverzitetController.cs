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

            if (x.Lat!=null && x.Lng!=null)
            {
                if (objekat.Lokacija == null)
                {
                    objekat.Lokacija = new Lokacija();
                }
                objekat.Lokacija.Lat = x.Lat.Value;
                objekat.Lokacija.Lng = x.Lng.Value;
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
    public ActionResult GetAll(int? pageNumber, int pageSize = 10)
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
                gradid=s.GradID,
                lokacija = s.Lokacija!=null? new {lat = s.Lokacija.Lat,lng = s.Lokacija.Lng } : null
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
        public ActionResult GetById(int id, int objavePageNumber = 1, int fakultetiPageNumber = 1)
        {
            var univerzitet = _dbContext.Univerzitet
                .Include(x=>x.Grad)
                .Include(x => x.Ocjene)
                .Include(x=>x.Lokacija)
                .Where(u => u.ID == id)
                .FirstOrDefault();

            if (univerzitet == null)
            {
                return NotFound();
            }

            var objave = _dbContext.ReferentUniverziteta
                .Where(r => r.Univerzitet.ID == id)
                .SelectMany(r => r.Objave)
                .Include(x => x.Kategorija);

            var objavePagedList = PagedList<Objava>.Create(objave, objavePageNumber, 4);

            var fakulteti = _dbContext.Fakultet
                .Where(f => f.UniverzitetID == id);

            var fakultetiPagedList = PagedList<Fakultet>.Create(fakulteti, fakultetiPageNumber, 5);

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
                objave = new
                {
                    TotalCount = objavePagedList.TotalCount,
                    PageSize = objavePagedList.PageSize,
                    DataItems = objavePagedList.DataItems.Select(o => new
                    {
                        id = o.ID,
                        naslov = o.Naslov,
                        slikaObjave = o.Slika,
                        sadrzaj = o.Sadrzaj,
                        kategorija = o.Kategorija.Naziv,
                        vrijemeObjave = o.VrijemeObjave.ToString("dd.MM.yyyy HH:mm")
                    })
                },
                fakulteti= new
                {
                    TotalCount = fakultetiPagedList.TotalCount,
                    PageSize = fakultetiPagedList.PageSize,
                    DataItems = fakultetiPagedList.DataItems.Select(f => new
                    {
                        id = f.ID,
                        naziv = f.Naziv,
                        adresa = f.Adresa,
                        email = f.Email,
                        link = f.Link,
                        logo = f.Logo
                    })
                },
                prosjecnaOcjena = univerzitet.Ocjene.Any() ? Math.Round(univerzitet.Ocjene.Average(o => o.Vrijednost),2) : 0,
                lokacija = univerzitet.Lokacija != null ? new { lat = univerzitet.Lokacija.Lat, lng = univerzitet.Lokacija.Lng } : null
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

        [HttpPost]
        public ActionResult OcijeniUniverzitet([FromBody] OcjenaVM x)
        {
            var univerzitet = _dbContext.Univerzitet
                .Include(u => u.Ocjene)
                .FirstOrDefault(f => f.ID == x.univerzitetId);
            var student = _dbContext.Student.FirstOrDefault(s => s.ID == x.studentId);

            if (univerzitet == null)
                return BadRequest("Univerzitet ne postoji");
            if (student == null)
                return BadRequest("Pograsan ID studenta");

            if (x.ocjena < 1 || x.ocjena > 5)
                return BadRequest("Ocjena mora biti između 1 i 5");

            var ocjena = univerzitet.Ocjene?.FirstOrDefault(o => o.StudentId == x.studentId);

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
                univerzitet.Ocjene.Add(ocjena);
            }
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
