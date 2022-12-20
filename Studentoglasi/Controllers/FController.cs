using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentOglasi.Data;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;

namespace StudentOglasi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FController : ControllerBase
    {

        private readonly AppDbContext _dbContext;

        public FController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;

        }

        [HttpGet]
        public ActionResult GetKategorije()
        {
            var data = _dbContext.Kategorija
                .OrderBy(s => s.Naziv)
                .Select(s => new
                {
                    id = s.ID,
                    naziv = s.Naziv

                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Fakultet
                .OrderBy(s => s.Naziv)
                .Select(s => new
                {
                    id = s.ID,
                    naziv = s.Naziv,
                    email = s.Email,
                    telefon = s.Telefon,
                    Veza = s.Link


                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }

        [HttpGet]
        public ActionResult GetFirme()
        {
            var data = _dbContext.Firma
                .OrderBy(s => s.Naziv)
                .Select(s => new
                {
                    id = s.ID,
                    naziv = s.Naziv
                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }

        [HttpGet]
        public ActionResult GetStipenditori()
        {
            var data = _dbContext.Stipenditor
                .OrderBy(s => s.Naziv)
                .Select(s => new
                {
                    id = s.ID,
                    naziv = s.Naziv
                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }

        [HttpGet]
        public ActionResult GetGradovi()
        {
            var data = _dbContext.Grad
                .OrderBy(s => s.Naziv)
                .Select(s => new
                {
                    id = s.ID,
                    naziv = s.Naziv
                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }
    }
}
