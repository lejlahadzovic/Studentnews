using System.ComponentModel.DataAnnotations.Schema;

namespace StudentOglasi.Models
{
    [Table("Student")]
    public class Student:Korisnik
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string BrojIndeksa { get; set; }
        public int GodinaStudija { get; set; }
        public string NacinStudiranja { get; set; }
        [ForeignKey("FakultetID")]
        public Fakultet Fakultet { get; set; }
        public int FakultetID { get; set; }
        public List<Rezervacija> Rezervacije { get; set; }
        public List<Ocjena> OcjenaSmjestaja { get; set; }
        public List<PrijavaPraksa> PrijavePrakse { get; set; }
        public List<PrijavaStipendija> PrijaveStipendije { get; set; }
    }
}
