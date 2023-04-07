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
    public class ObjavaController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ObjavaController(AppDbContext dbContext, IHostingEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public ActionResult Snimi([FromForm] ObjavaVM x)
        {
            try
            {
                bool edit = false;
                Objava objava = new Objava();

                if (x.id == 0)
                {
                    objava = new Objava();
                    _dbContext.Add(objava);
                    objava.VrijemeObjave = DateTime.Now;
                }
                else
                {
                    objava = _dbContext.Objava.Find(x.id);
                    edit = true;
                }

                objava.Naslov = x.naslov;
                objava.Sadrzaj = x.sadrzaj;
                objava.KategorijaID = x.kategorijaID;

                if (x.slika != null)
                {
                    if (x.slika.Length > 500 * 1000)
                        return BadRequest("maksimalna velicina fajla je 500 KB");

                    if (edit == true)
                    {
                        string webRootPath = _hostingEnvironment.WebRootPath;
                        var fullPath = webRootPath + "/Slike/" + objava.Slika;

                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }

                    string ekstenzija = Path.GetExtension(x.slika.FileName);
                    string contetntType = x.slika.ContentType;

                    var filename = $"{Guid.NewGuid()}{ekstenzija}";

                    var myFile = new FileStream(Config.SlikeFolder + filename, FileMode.Create);
                    x.slika.CopyTo(myFile);
                    objava.Slika = filename;
                    myFile.Close();
                }
                _dbContext.SaveChanges();
                return Ok(objava);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Objava
                .OrderBy(x => x.Naslov)
                .Select(x => new
                {
                    id = x.ID,
                    naslov = x.Naslov,
                    sadrzaj = x.Sadrzaj,
                    slika = Config.SlikePutanja + x.Slika,
                    kategorijaID = x.KategorijaID,
                    naziv_kategorije = x.Kategorija.Naziv,
                    vrijemeObjave = x.VrijemeObjave.ToString("dd.MM.yyyy H:mm")
                })
                .AsQueryable();
            return Ok(data);
        }

        [HttpGet]
        public ActionResult GetById(int id)
        {
            var data = _dbContext.Objava
                .Where(x => x.ID == id)
                .OrderBy(x => x.ID)
                .Select(x => new
                {
                    id = x.ID,
                    naslov = x.Naslov,
                    sadrzaj = x.Sadrzaj,
                    slika = Config.SlikePutanja + x.Slika,
                    naziv_kategorije = x.Kategorija.Naziv,
                    vrijemeObjave = x.VrijemeObjave.ToString("dd.MM.yyyy HH:mm"),
                    korisnik = x.ReferentFakulteta != null ? new {ime = x.ReferentFakulteta.Ime, prezime = x.ReferentFakulteta.Prezime, naziv=x.ReferentFakulteta.Fakultet.Naziv, slika=x.ReferentFakulteta.Slika} : 
                       x.ReferentUniverziteta!=null? new { ime = x.ReferentUniverziteta.Ime, prezime = x.ReferentUniverziteta.Prezime, naziv = x.ReferentUniverziteta.Univerzitet.Naziv, slika = x.ReferentUniverziteta.Slika } :
                       x.UposlenikFirme != null ? new { ime = x.UposlenikFirme.Ime, prezime = x.UposlenikFirme.Prezime, naziv = x.UposlenikFirme.Firma.Naziv, slika = x.UposlenikFirme.Slika } :
                       x.UposlenikStipenditora != null ? new { ime = x.UposlenikStipenditora.Ime, prezime = x.UposlenikStipenditora.Prezime, naziv = x.UposlenikStipenditora.Stipenditor.Naziv, slika = x.UposlenikStipenditora.Slika } :
                       null
                })
                .FirstOrDefault();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public ActionResult Obrisi([FromBody] int id)
        {
            Objava x = _dbContext.Objava.Find(id);

            string webRootPath = _hostingEnvironment.WebRootPath;
            var fullPath = webRootPath + "/Slike/" + x.Slika;

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            _dbContext.Remove(x);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
