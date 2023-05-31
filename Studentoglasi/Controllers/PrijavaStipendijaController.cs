using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using StudentOglasi.Data;
using StudentOglasi.Helper;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace StudentOglasi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PrijavaStipendijaController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public PrijavaStipendijaController(AppDbContext dbContext, IHostingEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public ActionResult Snimi([FromForm] PrijavaStipendijaVM x)
        {
            try
            {

                PrijavaStipendija prijava_stipendija = new PrijavaStipendija();

                 
                    
                
                _dbContext.Add(prijava_stipendija);
               

                prijava_stipendija.StipendijaID = x.StipendijaID;
                prijava_stipendija.Stipendija=_dbContext.Stipendija.Find(x.StipendijaID);
                prijava_stipendija.Student = _dbContext.Student.Find(x.StudentId);
                prijava_stipendija.StudentId = x.StudentId;
                prijava_stipendija.Dokumentacija= DodajFile.AddFileStipendija( Config.DokumentacijaFolder, prijava_stipendija, x.Dokumentacija);
                prijava_stipendija.CV= DodajFile.AddFileStipendija( Config.CVFolder, prijava_stipendija, x.CV);
                prijava_stipendija.ProsjekOcjena= DodajFile.AddFileStipendija(Config.ProsjekOcjenaFolder, prijava_stipendija, x.ProsjekOcjena);

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(prijava_stipendija, options);

                _dbContext.SaveChanges();
                return Ok(json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }
      

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.PrijavaStipendija
                .OrderBy(x => x.StudentId)
                .Select(x => new
                {
                    StipendijaId = x.StipendijaID,
                    Dokumentacija = x.Dokumentacija,
                    CV = x.CV,
                    ProsjekOcjena = x.ProsjekOcjena,
                    studentIme = x.Student.Ime,
                    studentID = x.StudentId
                })
                .AsQueryable();
            return Ok(data);
        }
        [HttpGet]
        public ActionResult GetByUposlenikId(int uposlenikID)
        {
            var data = _dbContext.PrijavaStipendija
                .Where(x => x.Stipendija.UposlenikID == uposlenikID)
                .GroupBy(x => x.Stipendija)
                .Select(group => new
                {
                    Stipendija = group.Key.Naslov,
                    Prijave = group.Select(x => new
                    {
                        student = x.Student.Ime + ' ' + x.Student.Prezime,
                        brojIndeksa = x.Student.BrojIndeksa,
                        dokumentacija = Config.DokumentacijaPutanja + x.Dokumentacija,
                        CV = Config.CVPutanja + x.CV,
                        prosjekOcjena = Config.ProsjekOcjenaPutanja + x.ProsjekOcjena
                    })
                })
                .AsQueryable();
            return Ok(data);
        }

        [HttpGet]
        public ActionResult GetByStudentId(int studentID)
        {
            var data = _dbContext.PrijavaStipendija
                .Where(x => x.StudentId == studentID)
                .Select(x => new
                {
                    stipendijaId=x.StipendijaID,
                    naslov =x.Stipendija.Naslov,
                    iznos=x.Stipendija.Iznos,
                    rokPrijave=x.Stipendija.RokPrijave,
                    dokumentacija = Config.DokumentacijaPutanja + x.Dokumentacija,
                    CV = Config.CVPutanja + x.CV,
                    prosjekOcjena = Config.ProsjekOcjenaPutanja + x.ProsjekOcjena
                })
                .ToList();
            return Ok(data);
        }

        [HttpPost]
        public ActionResult Obrisi(int studentId, int stipendijaId)
        {
            PrijavaStipendija prijava = _dbContext.PrijavaStipendija.FirstOrDefault(r => r.StudentId == studentId && r.StipendijaID == stipendijaId);

            if (prijava == null)
            {
                return NotFound();
            }

            _dbContext.Remove(prijava);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}



