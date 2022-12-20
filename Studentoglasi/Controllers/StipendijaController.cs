using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using StudentOglasi.Data;
using StudentOglasi.Helper;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace StudentOglasi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StipendijaController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;

        public StipendijaController(AppDbContext dbContext, IHostingEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpPost]
        public ActionResult Snimi([FromForm] StipendijaVM x)
        {
            try
            {
                bool edit = false;
                Stipendija stipendija = new Stipendija();

                if (x.id == 0)
                {
                    stipendija = new Stipendija();
                    _dbContext.Add(stipendija);
                    stipendija.VrijemeObjave = DateTime.Now;
                }
                else
                {
                    stipendija = _dbContext.Stipendija.Find(x.id);
                    edit = true;
                }

                stipendija.RokPrijave = x.rokPrijave;
                stipendija.Naslov = x.naslov;
                stipendija.Opis = x.opis;
                stipendija.Uslovi = x.uslovi;
                stipendija.Iznos = x.iznos;
                stipendija.Kriterij = x.kriterij;
                stipendija.PotrebnaDokumentacija = x.potrebnaDokumentacija;
                stipendija.Izvor = x.izvor;
                stipendija.NivoObrazovanja = x.nivoObrazovanja;
                stipendija.BrojStipendisata = x.brojStipendisata;
                stipendija.UposlenikID = x.uposlenikID;

                if (x.slika != null)
                {
                    if (x.slika.Length > 500 * 1000)
                        return BadRequest("max velicina fajla je 500 KB");

                    if (edit == true)
                    {
                        string webRootPath = _hostingEnvironment.WebRootPath;
                        var fullPath = webRootPath + "/Slike/" + stipendija.Slika;

                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }

                    string ekstenzija = Path.GetExtension(x.slika.FileName);

                    var filename = $"{Guid.NewGuid()}{ekstenzija}";

                    x.slika.CopyTo(new FileStream(Config.SlikeFolder + filename, FileMode.Create));
                    stipendija.Slika = filename;

                }
                _dbContext.SaveChanges();
                return Ok(stipendija);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Stipendija
                .OrderBy(x => x.ID)
                .Select(x => new
                {
                    id = x.ID,
                    naslov = x.Naslov,
                    rokPrijave = x.RokPrijave,
                    opis = x.Opis,
                    slika = Config.SlikePutanja + x.Slika,
                    vrijemeObjave = x.VrijemeObjave.ToString("dd.MM.yyyy H:mm"),
                    uposlenikID = x.UposlenikID,
                    ime_uposlenika = x.Uposlenik.Ime,
                    uslovi=x.Uslovi,
                    iznos=x.Iznos,
                    kriterij=x.Kriterij,
                    potrebnaDokumentacija=x.PotrebnaDokumentacija,
                    nivoObrazovanja=x.NivoObrazovanja,
                    brojStipendisata=x.BrojStipendisata,
                    naziv_stipenditora = x.Uposlenik.Stipenditor.Naziv,
                    stipenditorid = x.Uposlenik.Stipenditor.ID
                })
                .AsQueryable();
            return Ok(data);
        }

        [HttpPost]
        public ActionResult Obrisi([FromBody] int id)
        {
            Stipendija x = _dbContext.Stipendija.Find(id);

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
