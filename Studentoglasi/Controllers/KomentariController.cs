using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentOglasi.Data;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;

namespace StudentOglasi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class KomentariController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public KomentariController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;

        }
        
        [HttpPost]
        public ActionResult Snimi([FromBody] KomentarVM komentar)
        {
            Komentar? objekat;

            objekat = new Komentar();
            _dbContext.Add(objekat);

            if(komentar.objavaId!=null && komentar.objavaId>0)
                objekat.ObjavaID = komentar.objavaId;

            if (komentar.oglasId != null && komentar.oglasId > 0)
                objekat.OglasID = komentar.oglasId;

            if (komentar.komentarId != null && komentar.komentarId > 0)
                objekat.KomentarID = komentar.komentarId;

            objekat.KorisnikID = komentar.korisnikId;
            objekat.Text = komentar.text;
            objekat.VrijemeObjave = DateTime.Now;

            _dbContext.SaveChanges();
            return Ok(objekat);
        }

        [HttpGet]
        public ActionResult GetKomentari(int? objavaId, int? oglasId)
        {
            if(objavaId==null &&oglasId==null)
            {
                return BadRequest("Potrebno je proslijediti objavaId ili oglasId");
            }

            var komentari = _dbContext.Komentar
                .Include(k => k.Korisnik)
                .Where(k => (objavaId!=null && k.ObjavaID == objavaId)||
                            (oglasId != null && k.OglasID == oglasId))
                .OrderBy(k => k.ID)
                .ToList();

            var data = komentari.Where(k => k.KomentarID == null)
                .Select(k => new
                {
                    id = k.ID,
                    korisnik = k.Korisnik.isStudent ? new { ime = k.Korisnik.student.Ime, prezime = k.Korisnik.student.Prezime, slika = k.Korisnik.Slika } :
                        k.Korisnik.isReferentUniverziteta ? new { ime = k.Korisnik.referentUniverziteta.Ime, prezime = k.Korisnik.referentUniverziteta.Prezime, slika = k.Korisnik.Slika } :
                        k.Korisnik.isReferentFakulteta ? new { ime = k.Korisnik.referentFakulteta.Ime, prezime = k.Korisnik.referentFakulteta.Prezime, slika = k.Korisnik.Slika } :
                        k.Korisnik.isIzdavacSmjestaja ? new { ime = k.Korisnik.izdavac.Ime, prezime = k.Korisnik.izdavac.Prezime, slika = k.Korisnik.Slika } :
                        k.Korisnik.isUposlenikFirme ? new { ime = k.Korisnik.uposlenikFirme.Ime, prezime = k.Korisnik.uposlenikFirme.Prezime, slika = k.Korisnik.Slika } :
                        k.Korisnik.isUposlenikStipenditora ? new { ime = k.Korisnik.uposlenikStipenditora.Ime, prezime = k.Korisnik.uposlenikStipenditora.Prezime, slika = k.Korisnik.Slika } :
                        k.Korisnik.isAdmin ? new { ime = k.Korisnik.admin.Ime, prezime = k.Korisnik.admin.Prezime, slika = k.Korisnik.Slika } :
                        null,
                    vrijemeObjave = k.VrijemeObjave.ToString("H:mm dd.MM.yyyy"),
                    komentar = k.Text,
                    odgovori = komentari.Where(o => o.KomentarID == k.ID)
                        .Select(o => new
                        {
                            korisnik = o.Korisnik.isStudent ? new { ime = o.Korisnik.student.Ime, prezime = o.Korisnik.student.Prezime, slika = o.Korisnik.Slika } :
                                o.Korisnik.isReferentUniverziteta ? new { ime = o.Korisnik.referentUniverziteta.Ime, prezime = o.Korisnik.referentUniverziteta.Prezime, slika = o.Korisnik.Slika } :
                                o.Korisnik.isReferentFakulteta ? new { ime = o.Korisnik.referentFakulteta.Ime, prezime = o.Korisnik.referentFakulteta.Prezime, slika = o.Korisnik.Slika } :
                                o.Korisnik.isIzdavacSmjestaja ? new { ime = o.Korisnik.izdavac.Ime, prezime = o.Korisnik.izdavac.Prezime, slika = o.Korisnik.Slika } :
                                o.Korisnik.isUposlenikFirme ? new { ime = o.Korisnik.uposlenikFirme.Ime, prezime = o.Korisnik.uposlenikFirme.Prezime, slika = o.Korisnik.Slika } :
                                o.Korisnik.isUposlenikStipenditora ? new { ime = o.Korisnik.uposlenikStipenditora.Ime, prezime = o.Korisnik.uposlenikStipenditora.Prezime, slika = o.Korisnik.Slika } :
                                o.Korisnik.isAdmin ? new { ime = o.Korisnik.admin.Ime, prezime = o.Korisnik.admin.Prezime, slika = o.Korisnik.Slika } :
                                null,
                            vrijemeObjave = o.VrijemeObjave.ToString("H:mm dd.MM.yyyy"),
                            komentar = o.Text,
                        })
                })
                .ToList();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
    }
}
