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
    }
}
