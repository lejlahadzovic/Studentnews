using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentOglasi.Models
{
    public class Fakultet
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string? Opis { get; set; }
        public string? Logo { get; set; }
        public string? Slika { get; set; }
        public string Link { get; set; }
        [ForeignKey("UniverzitetID")]
        public Univerzitet Univerzitet { get; set; }
        public int UniverzitetID { get; set; }
        public List<Ocjena> Ocjene { get; set; }
    }
}
