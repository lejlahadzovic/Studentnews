﻿using Microsoft.AspNetCore.Http;
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
                stipendija.Slika = DodajFile.UploadSlike(x.slika, stipendija.Slika, _hostingEnvironment);

                _dbContext.SaveChanges();
                return Ok(stipendija);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }

        [HttpGet]
        public ActionResult GetAll(int? uposlenikID, int? pageNumber, int pageSize = 10)
        {
            var data = _dbContext.Stipendija
                .Where(x => uposlenikID == null || x.UposlenikID == uposlenikID)
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
            var data = _dbContext.Stipendija
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
                    uposlenik = x.Uposlenik.Ime + ' ' + x.Uposlenik.Prezime,
                    uposlenik_slika = x.Uposlenik.Slika,
                    uslovi = x.Uslovi,
                    iznos = x.Iznos,
                    kriterij = x.Kriterij,
                    potrebnaDokumentacija = x.PotrebnaDokumentacija,
                    nivoObrazovanja = x.NivoObrazovanja,
                    brojStipendisata = x.BrojStipendisata,
                    naziv_stipenditora = x.Uposlenik.Stipenditor.Naziv,
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
            Stipendija x = _dbContext.Stipendija.Find(id);

            DodajFile.ObrisiSliku(x.Slika, _hostingEnvironment);

            _dbContext.Remove(x);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
