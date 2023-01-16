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
            data.Add("Univerzitet", _dbContext.Univerzitet.Count());
            data.Add("Student", _dbContext.Student.Count());
            data.Add("Fakultet", _dbContext.Fakultet.Count());
            data.Add("Administrator", _dbContext.Administrator.Count());
            data.Add("IzdavacSmjestaja", _dbContext.IzdavacSmjestaja.Count());
            data.Add("Praksa", _dbContext.Praksa.Count());
            data.Add("ReferentFakultet", _dbContext.ReferentFakulteta.Count());
            data.Add("ReferentUniverziteta", _dbContext.ReferentUniverziteta.Count());
            data.Add("Smjestaj", _dbContext.Smjestaj.Count());
            data.Add("UposlenikFirme", _dbContext.UposlenikFirme.Count());
            data.Add("Stipenditor", _dbContext.Stipenditor.Count());
            data.Add("UposlenikStipenditora", _dbContext.UposlenikStipenditora.Count());
            data.Add("Stipendija",_dbContext.Stipendija.Count());
            return Ok(data);
        }

        [HttpPost]
        public ActionResult Generisi()
        {
            var gradovi = new List<Grad>();
            var firme = new List<Firma>();
            var studenti = new List<Student>();
            var univerzitet = new List<Univerzitet>();
            var fakulteti = new List<Fakultet>();
         
            var administratori=new List<Administrator>();
            var izdavaci = new List<IzdavacSmjestaja>();
            var praksa = new List<Praksa>();
            var referentF = new List<ReferentFakulteta>();
            var referentU = new List<ReferentUniverziteta>();
            var smjestaj = new List<Smjestaj>();
           
            var uposlenikF=new List<UposlenikFirme>();
            var uposlenikS = new List<UposlenikStipenditora>();
            var stipenditori= new List<Stipenditor>();
            var stipendije=new List<Stipendija>();

            Random rng = new Random();
            int number = rng.Next(1, 1000000000);
            string digits = number.ToString("000000000");

            administratori.Add(new Administrator { Ime = TokenGenerator.GenerisiIme(5),
                Prezime = TokenGenerator.GenerisiIme(5),
                    Email = TokenGenerator.GenerisiIme(5) + "@gmail.com", Username = TokenGenerator.GenerisiIme(5),
                    Password = "test"});
          
            for (int i = 0; i < 10; i++)
            {

                izdavaci.Add(new IzdavacSmjestaja
                {
                    Ime = TokenGenerator.GenerisiIme(5),
                    Prezime = TokenGenerator.GenerisiIme(5),
                    Email = TokenGenerator.GenerisiIme(5) + "@gmail.com",
                    Username = TokenGenerator.GenerisiIme(5),
                    Password = "test",
                    Telefon=digits

                });
            }
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
            univerzitet.Add(new Univerzitet { Naziv = "Univerzitet u Zenici", Email = "unze@unze.ba", Telefon = "032449126", Link = "http://unze.ba/", Grad = gradovi[1]});
            univerzitet.Add(new Univerzitet { Naziv = "Univerzitet u Sarajevu", Email = "us@gmail.ba", Telefon = "0300000", Link = "http://ptf.us.ba/", Grad = gradovi[2]});
            fakulteti.Add(new Fakultet { Naziv = "Politehnički fakultet", Email = "slemes@unze.ba",Adresa="adresaa 1", Telefon = "032449126", Link = "http://ptf.unze.ba/", Univerzitet = univerzitet[0]});
            fakulteti.Add(new Fakultet { Naziv = "Fakultet političkih nauka", Email = "fpk@unze.ba", Adresa = "adresaa 1", Telefon = "032449126", Link = "http://ptf.unze.ba/", Univerzitet = univerzitet[0] });

            for (int i = 0; i < 10; i++)
            {
                stipenditori.Add(new Stipenditor
                {
                    Naziv = TokenGenerator.GenerisiIme(5),
                    Adresa = TokenGenerator.GenerisiIme(5),
                    Link = "www. "+ TokenGenerator.GenerisiIme(5)+".com",
                    Email = TokenGenerator.GenerisiIme(5) + "@gmail.com",
                    TipUstanove = "javna ustanova",
                    Grad = gradovi[0]
                });
            }

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

            for (int i = 0; i < 10; i++)
            {
                uposlenikF.Add(new UposlenikFirme
                {
                    Ime = TokenGenerator.GenerisiIme(5),
                    Prezime = TokenGenerator.GenerisiIme(5),
                    Pozicija = " ",
                    Email = TokenGenerator.GenerisiIme(5) + "@gmail.com",
                    Username = TokenGenerator.GenerisiIme(5),
                    Password = "test",
                    Firma = firme[0]
                });
            }
            for (int i = 0; i < 10; i++)
            {
                uposlenikS.Add(new UposlenikStipenditora
                {
                    Ime = TokenGenerator.GenerisiIme(5),
                    Prezime = TokenGenerator.GenerisiIme(5),
                    Email = TokenGenerator.GenerisiIme(5) + "@gmail.com",
                    Username = TokenGenerator.GenerisiIme(5),
                    Password = "test",
                    Stipenditor = stipenditori[0]
                });
            }

            for (int i = 0; i < 5; i++)
            {
                praksa.Add(new Praksa { Naslov = " Praksa" + i.ToString(), Slika = "00c19756-256e-4f70-bfd2-0caf3d18d747.png", RokPrijave = new DateTime(2023, 02, 25), Opis = "opis oglasa ", VrijemeObjave = DateTime.Now, PocetakPrakse = new DateTime(2023, 02, 25), KrajPrakse = new DateTime(2023, 03, 25), Kvalifikacije = " ", Benefiti = " ", Placena = true, Uposlenik = uposlenikF[1] });
            }
            for (int i = 0; i < 5; i++)
            {
                praksa.Add(new Praksa { Naslov = " Praksa"+i.ToString(), Slika = "00c19756-256e-4f70-bfd2-0caf3d18d747.png", RokPrijave = new DateTime(2023, 10, 10), Opis = "opis oglasa ", VrijemeObjave = DateTime.Now, PocetakPrakse = new DateTime(2023, 12, 05), KrajPrakse = new DateTime(2023, 06, 20), Kvalifikacije = " ", Benefiti = " ", Placena = false, Uposlenik = uposlenikF[2] });
            }
            for (int i = 0; i < 10; i++)
            {
                referentF.Add(new ReferentFakulteta 
                {
                    Ime = TokenGenerator.GenerisiIme(5),
                    Prezime = TokenGenerator.GenerisiIme(5),
                    Email = TokenGenerator.GenerisiIme(5) + "@gmail.com",
                    Username = TokenGenerator.GenerisiIme(5),
                    Password = "test",
                    Fakultet = fakulteti[0]
                });
            }

            for (int i = 0; i < 10; i++)
            {
                referentU.Add(new ReferentUniverziteta
                {
                    Ime = TokenGenerator.GenerisiIme(5),
                    Prezime = TokenGenerator.GenerisiIme(5),
                    Email = TokenGenerator.GenerisiIme(5) + "@gmail.com",
                    Username = TokenGenerator.GenerisiIme(5),
                    Password = "test",
                    Univerzitet = univerzitet[0]
                });
            }

            for (int i = 0; i < 10; i++) {
                stipendije.Add(new Stipendija {Naslov=" Stipendija", Slika = "00c19756-256e-4f70-bfd2-0caf3d18d747.png", RokPrijave= new DateTime(2023, 02, 25),Opis="opis oglasa ",VrijemeObjave=DateTime.Now, Uslovi=" ",Iznos=i+1000.00,Kriterij=" ",PotrebnaDokumentacija=" ",Izvor=" ",NivoObrazovanja=" ",BrojStipendisata= i + 10, Uposlenik = uposlenikS[1] });
            }
               for (int i = 0; i < 10; i++) {
                smjestaj.Add(new Smjestaj { Naslov = " Smještaj", Slika= "00c19756-256e-4f70-bfd2-0caf3d18d747.png",
                    RokPrijave = new DateTime(2023, 02, 25), Opis = "opis oglasa ", VrijemeObjave = DateTime.Now,
                    Cijena =i+100,BrojSoba=2,DodatneUsluge="... ",NacinGrijanja="centralno grijanje",
                    Grad = gradovi[1], Izdavac = izdavaci[1] });
            }   

            _dbContext.AddRange(firme);
            _dbContext.AddRange(gradovi);
            _dbContext.AddRange(univerzitet);
            _dbContext.AddRange(fakulteti);
            _dbContext.AddRange(studenti);
            _dbContext.AddRange(uposlenikF);
            _dbContext.AddRange(administratori);
            _dbContext.AddRange(izdavaci);
            _dbContext.AddRange(praksa);
            _dbContext.AddRange(fakulteti);
            _dbContext.AddRange(stipenditori);
            _dbContext.AddRange(uposlenikS);
            _dbContext.AddRange(referentU);
            _dbContext.AddRange(referentF);
            _dbContext.AddRange(smjestaj);
            _dbContext.AddRange(stipendije);

            _dbContext.SaveChanges();

            return Count();
        }


    }
}
