using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentOglasi.Models
{
    public class Firma
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Adresa { get; set; }
        public string Link { get; set; }
        [ForeignKey("GradID")]
        public Grad Grad { get; set; }
        public int GradID { get; set; }
        public List<Ocjena> Ocjene { get; set; }
    }
}
