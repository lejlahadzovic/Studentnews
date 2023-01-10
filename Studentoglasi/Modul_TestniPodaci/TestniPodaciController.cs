using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentOglasi.Data;
using StudentOglasi.Helper;
using StudentOglasi.Models;

namespace StudentOglasi.Modul_TestniPodaci
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestniPodaciController : Controller
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
            
            data.Add("Drzava", _dbContext.Grad.Count());
            data.Add("Ocjena", _dbContext.Ocjena.Count());
            data.Add("Univerzitet", _dbContext.Univerzitet.Count());
            //data.Add("Student", _dbContext.Student.Count());
            //data.Add("Fakultet", _dbContext.Fakultet.Count());
            return Ok(data);
        }

        [HttpPost]
        public ActionResult Generisi()
        {
            var gradovi = new List<Grad>();
          
            //var studenti = new List<Student>();
            //var fakulteti = new List<Fakultet>();
            
            gradovi.Add(new Grad { Naziv = "Banja Luka" });
            gradovi.Add(new Grad { Naziv = "Sarajevo" }); 
            gradovi.Add(new Grad { Naziv = "Tuzla" });
            gradovi.Add(new Grad { Naziv = "Mostar" });  
            gradovi.Add(new Grad { Naziv = "Zenica" });
            gradovi.Add(new Grad { Naziv = "Bijeljina" });
            gradovi.Add(new Grad { Naziv = "Bihać" });  
            gradovi.Add(new Grad { Naziv = "Istočno Sarajevo" });
            gradovi.Add(new Grad { Naziv = "Bileća" });
            gradovi.Add(new Grad { Naziv = "Brčko" });
            gradovi.Add(new Grad { Naziv = "Bugojno" });
         
            gradovi.Add(new Grad { Naziv = "Bosanska Krupa" });
            gradovi.Add(new Grad { Naziv = "Bosanska Gradiška" });
            gradovi.Add(new Grad { Naziv = "Cazin" });
            gradovi.Add(new Grad { Naziv = "Čapljina" });
            gradovi.Add(new Grad { Naziv = "Derventa" });
            gradovi.Add(new Grad { Naziv = "Doboj" });
            gradovi.Add(new Grad { Naziv = "Foča" });
            gradovi.Add(new Grad { Naziv = "Goražde" });
            gradovi.Add(new Grad { Naziv = "Gračanica" });
            gradovi.Add(new Grad { Naziv = "Gradačac" });
            gradovi.Add(new Grad { Naziv = "Gradiška" });

            gradovi.Add(new Grad { Naziv = "Konjic" });
            gradovi.Add(new Grad { Naziv = "Kakanj" });
            gradovi.Add(new Grad { Naziv = "Kiseljak" });
            gradovi.Add(new Grad { Naziv = "Laktaši" });
            gradovi.Add(new Grad { Naziv = "Livno" });
            gradovi.Add(new Grad { Naziv = "Lukavac" });
            gradovi.Add(new Grad { Naziv = "Ljubuški" });
          
            gradovi.Add(new Grad { Naziv = "Mrkonjić Grad" });
            gradovi.Add(new Grad { Naziv = "Novi Travnik" });
            gradovi.Add(new Grad { Naziv = "Orašje" });
            gradovi.Add(new Grad { Naziv = "Odžak" });
            gradovi.Add(new Grad { Naziv = "Prijedor" });
            gradovi.Add(new Grad { Naziv = "Prozor" });
           
            gradovi.Add(new Grad { Naziv = "Sanski Most" });
            gradovi.Add(new Grad { Naziv = "Srebrenik" });
            gradovi.Add(new Grad { Naziv = "Stolac" });
            gradovi.Add(new Grad { Naziv = "Šipovo" });
            gradovi.Add(new Grad { Naziv = "Široki Brijeg" });
            gradovi.Add(new Grad { Naziv = "Trebinje" });

            gradovi.Add(new Grad { Naziv = "Travnik" });
            gradovi.Add(new Grad { Naziv = "Visoko" });
            gradovi.Add(new Grad { Naziv = "Velika Kladuša" });
            gradovi.Add(new Grad { Naziv = "Zavidovići" });

            gradovi.Add(new Grad { Naziv = "Zvornik" });
            gradovi.Add(new Grad { Naziv = "Živinice" });
            
        

            
        
      

            _dbContext.SaveChanges();

            return Count();
        }


    }
}
