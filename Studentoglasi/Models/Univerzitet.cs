using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentOglasi.Models
{
    public class Univerzitet
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string? Logo { get; set; }
        public string? Slika { get; set; }
        public string Link { get; set; }
        [ForeignKey("GradID")]
        public Grad Grad { get; set; }
        public int GradID { get; set; }
        public ICollection<Ocjena> Ocjene { get; set; } = new List<Ocjena>();
        
        public Lokacija Lokacija { get; set; }
        public int? LokacijaID { get; set; }
    }
}
