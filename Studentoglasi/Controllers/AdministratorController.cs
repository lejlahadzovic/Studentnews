using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentOglasi.Data;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;
using StudentOglasi.Helper;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace StudentOglasi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AdministratorController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public AdministratorController(AppDbContext dbContext, IHostingEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public ActionResult Snimi([FromForm] AdministratorVM x)
        {
            try
            {
                Administrator? admin;
                admin = _dbContext.Administrator.Find(x.id);

                if (admin != null)
                {
                    admin.Username = x.username;
                    admin.Password = x.password;
                    admin.Ime = x.ime;
                    admin.Prezime = x.prezime;
                    admin.Email = x.email;
                    if (x.slika != null)
                    {
                        if (x.slika.Length > 500 * 1000)
                            return BadRequest("maksimalna velicina fajla je 500 KB");
                        string webRootPath = _hostingEnvironment.WebRootPath;
                        var fullPath = webRootPath + "/Slike/" + admin.Slika;

                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }

                        string ekstenzija = Path.GetExtension(x.slika.FileName);
                        string contetntType = x.slika.ContentType;

                        var filename = $"{Guid.NewGuid()}{ekstenzija}";

                        var myFile = new FileStream(Config.SlikeFolder + filename, FileMode.Create);
                        x.slika.CopyTo(myFile);
                        admin.Slika = filename;
                        myFile.Close();
                    }
                }

                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }

        [HttpGet]
        public ActionResult GetByID(int id)
        {
            var x = _dbContext.Administrator.FirstOrDefault(x => x.ID == id);
            if (x != null)
            {
                var administrator = new
                {
                    id = x.ID,
                    username = x.Username,
                    password = x.Password,
                    ime = x.Ime,
                    slika = x.Slika,
                    prezime = x.Prezime,
                    email = x.Email
                };
                return Ok(administrator);
            }

            else return BadRequest();
        }
    }
}
