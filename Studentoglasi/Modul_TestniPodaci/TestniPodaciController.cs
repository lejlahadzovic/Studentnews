using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentOglasi.Data;
using StudentOglasi.Helper;
using StudentOglasi.Models;
using StudentOglasi.ViewModels;

namespace StudentOglasi.Modul_TestniPodaci
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestniPodaciController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public TestniPodaciController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public ActionResult Count()
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            
            data.Add("Grad", _dbContext.Grad.Count());
            data.Add("Firma", _dbContext.Firma.Count());
           data.Add("Ocjena", _dbContext.Ocjena.Count());
            data.Add("Univerzitet", _dbContext.Univerzitet.Count());
            data.Add("Student", _dbContext.Student.Count());
            data.Add("Fakultet", _dbContext.Fakultet.Count());
            data.Add("Stipendija",_dbContext.Stipendija.Count());
            return Ok(data);
        }

        [HttpPost]
        public ActionResult Generisi()
        {
            var gradovi = new List<Grad>();
            var firme = new List<Firma>();
            var ocjene = new List<Ocjena>();
            var studenti = new List<Student>();
            var univerzitet = new List<Univerzitet>();
            var fakulteti = new List<Fakultet>();
            var stipednije = new List<Stipendija>();

            gradovi.Add(new Grad { Naziv = "Banja Luka" });
            gradovi.Add(new Grad { Naziv = "Sarajevo" }); 
            gradovi.Add(new Grad { Naziv = "Tuzla" });
            gradovi.Add(new Grad { Naziv = "Mostar" });  
            gradovi.Add(new Grad { Naziv = "Zenica" });
            gradovi.Add(new Grad { Naziv = "Bijeljina" });
            gradovi.Add(new Grad { Naziv = "Bihać" });  
            gradovi.Add(new Grad { Naziv = "Konjic" });
         
            gradovi.Add(new Grad { Naziv = "Goražde" });
            // for (int i = 0; i < 5; i++)
            //{
            //    ocjene.Add(new Ocjena { Student = studenti[i], Vrijednost=i});
            //}
            univerzitet.Add(new Univerzitet { Naziv = "Univerzitet u Zenici", Email = "unze@unze.ba", Telefon = "032449126", Link = "http://unze.ba/", Grad = gradovi[7],GradID=7 });
            univerzitet.Add(new Univerzitet { Naziv = "Univerzitet u Sarajevu", Email = "us@gmail.ba", Telefon = "0300000", Link = "http://ptf.us.ba/", Grad = gradovi[1], GradID = 1});
            fakulteti.Add(new Fakultet { Naziv = "Politehnički fakultet", Email = "slemes@unze.ba",Adresa="adresaa 1", Telefon = "032449126", Link = "http://ptf.unze.ba/", Univerzitet = univerzitet[1],UniverzitetID=1 });

            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                studenti.Add(new Student
                {
                    Ime = TokenGenerator.GenerisiIme(5),
                 Prezime = TokenGenerator.GenerisiIme(5),
                    BrojIndeksa = $"IB200{i:d}",
                    GodinaStudija=1,
                    NacinStudiranja="DL",
                    Username = TokenGenerator.GenerisiIme(5),
                    Password = "test",
                    Fakultet = fakulteti[0]
                });
            }

            firme.Add(new Firma { Naziv = "Atlantbh", Telefon= "033 716-550", Email= "contact@atlantbh.com", Adresa= "Bosmal City Center, Milana Preloga 12A", Link = "https://www.atlantbh.com/", Grad = gradovi[3] });
            firme.Add(new Firma { Naziv = "Atlantbh", Telefon = "033 716-550", Email = "contact@atlantbh.com", Adresa = "Bosmal City Center, Milana Preloga 12A", Link = "https://www.atlantbh.com/" , Grad = gradovi[4] });
            
          



            _dbContext.AddRange(firme);
            _dbContext.AddRange(gradovi);
            _dbContext.AddRange(univerzitet);
            _dbContext.AddRange(fakulteti);
            _dbContext.AddRange(studenti);
            _dbContext.AddRange(stipednije);
            _dbContext.SaveChanges();

            return Count();
        }


    }
}
