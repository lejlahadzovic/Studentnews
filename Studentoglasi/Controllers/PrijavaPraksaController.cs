using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using StudentOglasi.Data;
using StudentOglasi.Helper;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StudentOglasi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PrijavaPraksaController : Controller
    {
          private readonly AppDbContext _dbContext;
          private readonly IHostingEnvironment _hostingEnvironment;
        public PrijavaPraksaController(AppDbContext dbContext, IHostingEnvironment hostingEnvironment)
        {
                _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }

            [HttpPost]
            public ActionResult Snimi([FromForm] PrijavaPraksaVM x)
            {
                try
                {
                   
                    PrijavaPraksa prijava_praksa = new PrijavaPraksa();

                _dbContext.Add(prijava_praksa);
               

                prijava_praksa.PraksaId = x.PraksaId;
                prijava_praksa.Praksa = _dbContext.Praksa.Find(x.PraksaId);
                prijava_praksa.Student = _dbContext.Student.Find(x.StudentId);
                prijava_praksa.StudentId = x.StudentId;
                prijava_praksa.PropratnoPismo = DodajFile.AddFilePraksa(Config.DokumentacijaFolder, prijava_praksa, x.propratnoPismo);
                prijava_praksa.CV = DodajFile.AddFilePraksa(Config.CVFolder, prijava_praksa, x.CV);
                prijava_praksa.Certifikati = DodajFile.AddFilePraksa(Config.ProsjekOcjenaFolder, prijava_praksa, x.certifikati);

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(prijava_praksa, options);

                _dbContext.SaveChanges();
                FirebaseCloudMessaging.SendNotification("Prijava ", "Vaša prijava je uspješno zabilježena", "success");
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
                var data = _dbContext.PrijavaPraksa
                    .OrderBy(x => x.StudentId)
                    .Select(x => new
                    {
                        praksaID = x.PraksaId,
                        propratnoPismo = x.PropratnoPismo,
                        CV = x.CV,
                        certifikati = x.Certifikati,
                        studentIme=x.Student.Ime,
                        studentID=x.StudentId
                    })
                    .AsQueryable();
                return Ok(data);
            }

            [HttpGet]
            public ActionResult GetByUposlenikId(int uposlenikID)
            {
                var data = _dbContext.PrijavaPraksa
                    .Where(x=>x.Praksa.UposlenikID == uposlenikID)
                    .GroupBy(x => x.Praksa)
                    .Select(group => new
                    {
                        Praksa = group.Key.Naslov, 
                        Prijave = group.Select(x => new
                        {
                            student = x.Student.Ime +' '+x.Student.Prezime,
                            brojIndeksa=x.Student.BrojIndeksa,
                            propratnoPismo = Config.DokumentacijaPutanja + x.PropratnoPismo,
                            CV = Config.CVPutanja + x.CV,
                            certifikati = Config.ProsjekOcjenaPutanja + x.Certifikati
                        })
                    })
                    .AsQueryable();
                return Ok(data);
            }

            [HttpGet]
            public ActionResult GetByStudentId(int studentID)
            {
                var data = _dbContext.PrijavaPraksa
                    .Where(x => x.StudentId == studentID)
                    .Select(x => new
                    {
                        praksaId=x.PraksaId,
                        naslov = x.Praksa.Naslov,
                        pocetakPrakse = x.Praksa.PocetakPrakse,
                        krajPrakse = x.Praksa.KrajPrakse,
                        placena = x.Praksa.Placena,
                        propratnoPismo = Config.DokumentacijaPutanja + x.PropratnoPismo,
                        CV = Config.CVPutanja + x.CV,
                        certifikati = Config.ProsjekOcjenaPutanja + x.Certifikati
                    })
                    .ToList();
                return Ok(data);
            }
        [HttpPost]
        public ActionResult Obrisi(int studentId, int praksaId)
        {
            PrijavaPraksa prijava = _dbContext.PrijavaPraksa.FirstOrDefault(r => r.StudentId == studentId && r.PraksaId == praksaId);

            if (prijava == null)
            {
                return NotFound();
            }

            _dbContext.Remove(prijava);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("{id}")]
        public ActionResult AddCV(int id, [FromForm] PrijavaPraksaVM x)
        {
            try
            {
                PrijavaPraksa prijava = _dbContext.PrijavaPraksa.FirstOrDefault(p => p.StudentId == id);

                if (x.CV != null && prijava != null)
                {
                    if (x.CV.Length > 300 * 1000)
                        return BadRequest("max velicina fajla je 300 KB");

                    string ekstenzija = Path.GetExtension(x.CV.FileName);

                    var filename = $"{Guid.NewGuid()}{ekstenzija}";

                    x.CV.CopyTo(new FileStream(Config.CVFolder + filename, FileMode.Create));
                    prijava.CV = Config.CVPutanja + filename;
                    _dbContext.SaveChanges();
                }

                return Ok(prijava);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }
    }
    }



