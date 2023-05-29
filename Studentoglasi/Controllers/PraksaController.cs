using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentOglasi.Data;
using StudentOglasi.Helper;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
namespace StudentOglasi.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class PraksaController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PraksaController(AppDbContext dbContext, IHostingEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public ActionResult Snimi([FromForm] PraksaVM x)
        {
            try
            {
                bool edit = false;
                Praksa praksa = new Praksa();

                if (x.id == 0)
                {
                    praksa = new Praksa();
                    _dbContext.Add(praksa);
                    praksa.VrijemeObjave = DateTime.Now;
                }
                else
                {
                    praksa = _dbContext.Praksa.Find(x.id);
                    edit = true;
                }

                praksa.RokPrijave = x.rokPrijave;
                praksa.Naslov = x.naslov;
                praksa.Opis = x.opis;
                praksa.KrajPrakse = x.krajPrakse;
                praksa.PocetakPrakse = x.pocetakPrakse;
                praksa.Kvalifikacije = x.kvalifikacije;
                praksa.Benefiti = x.benefiti;
                praksa.Placena = x.placena;
                praksa.UposlenikID = x.uposlenikID;

                if (x.slika != null)
                {
                    if (x.slika.Length > 500 * 1000)
                        return BadRequest("max velicina fajla je 500 KB");

                    if (edit == true)
                    {
                        string webRootPath = _hostingEnvironment.WebRootPath;
                        var fullPath = webRootPath + "/Slike/" + praksa.Slika;

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
                    praksa.Slika = filename;

                    myFile.Close();
                }
                _dbContext.SaveChanges();
                return Ok(praksa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }

        [HttpGet]
        public ActionResult GetAll(int? uposlenikID,int? pageNumber, int pageSize=10)
        {
            var data = _dbContext.Praksa
                .Where(x => uposlenikID == null || x.UposlenikID == uposlenikID)
                .OrderBy(x => x.ID)
                .Select(x => new
                {
                    id = x.ID,
                    naslov=x.Naslov,
                    rokPrijave = x.RokPrijave,
                    pocetakPrakse=x.PocetakPrakse,
                    krajPrakse=x.KrajPrakse,
                    opis = x.Opis,
                    slika = Config.SlikePutanja + x.Slika,
                    kvalifikacije=x.Kvalifikacije,
                    benefiti=x.Benefiti,
                    vrijemeObjave = x.VrijemeObjave.ToString("dd.MM.yyyy H:mm"),
                    placena=x.Placena,
                    uposlenikID=x.UposlenikID,
                    ime_uposlenika=x.Uposlenik.Ime,
                    naziv_firme=x.Uposlenik.Firma.Naziv,
                    firmaid = x.Uposlenik.Firma.ID
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
        public ActionResult GetById(int id)
        {
            var data = _dbContext.Praksa
                .Where(x => x.ID == id)
                .Select(x => new
                {
                    id = x.ID,
                    naslov = x.Naslov,
                    rokPrijave = x.RokPrijave,
                    pocetakPrakse = x.PocetakPrakse,
                    krajPrakse = x.KrajPrakse,
                    opis = x.Opis,
                    slika = Config.SlikePutanja + x.Slika,
                    kvalifikacije = x.Kvalifikacije,
                    benefiti = x.Benefiti,
                    vrijemeObjave = x.VrijemeObjave.ToString("dd.MM.yyyy H:mm"),
                    placena = x.Placena,
                    uposlenik = x.Uposlenik.Ime + ' ' + x.Uposlenik.Prezime,
                    naziv_firme = x.Uposlenik.Firma.Naziv,
                    uposlenik_slika= x.Uposlenik.Slika
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
            Praksa x = _dbContext.Praksa.Find(id);

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
