using Microsoft.AspNetCore.Mvc;
using StudentOglasi.Data;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;

namespace StudentOglasi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public StudentController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult Snimi([FromBody] StudentVM x)
        {
            Student student;
            if (x.id == 0)
            {
                student = new Student();
                _dbContext.Add(student);
            }
            else
            {
                student = _dbContext.Student.Find(x.id);
            }
            student.Username = x.username;
            student.Password = x.password;
            student.Ime = x.ime;
            student.Prezime = x.prezime;
            student.BrojIndeksa = x.broj_indeksa;
            student.FakultetID = x.fakultetID;
            student.GodinaStudija = x.godinaStudija;
            student.NacinStudiranja = x.nacin_studiranja;

            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult<List<StudentVM>> GetAll()
        {
            var data = _dbContext.Student.
                OrderBy(s => s.BrojIndeksa)
                .Select(s => new StudentVM
                {
                    id = s.ID,
                    password = s.Password,
                    username = s.Username,
                    ime = s.Ime,
                    prezime = s.Prezime,
                    broj_indeksa = s.BrojIndeksa,
                    nacin_studiranja = s.NacinStudiranja,
                    godinaStudija = s.GodinaStudija,
                    fakultetID = s.FakultetID,
                    naziv_fakulteta = s.Fakultet.Naziv
                });
            return data.ToList();
        }

        [HttpPost]
        public ActionResult Obrisi([FromBody] StudentVM student)
        {
            Student s = _dbContext.Student.Find(student.id);
            _dbContext.Remove(s);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
