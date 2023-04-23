using System.ComponentModel.DataAnnotations.Schema;

namespace StudentOglasi.Models
{
    [Table("Smjestaj")]
    public class Smjestaj : Oglas
    {
        public double Cijena { get; set; }
        public int Kapacitet { get; set; }
        public string DodatneUsluge { get; set; }
        public int BrojSoba { get; set; }
        public bool Parking { get; set; }
        public string NacinGrijanja { get; set; }
        [ForeignKey("GradID")]
        public Grad Grad { get; set; }
        public int GradID { get; set; }
        [ForeignKey("IzdavacID")]
        public IzdavacSmjestaja Izdavac { get; set; }
        public int IzdavacID { get; set; }
        public List<Rezervacija> Rezervacije { get; set; }
        public ICollection<Ocjena> Ocjene { get; set; } = new List<Ocjena>();
    }
}
