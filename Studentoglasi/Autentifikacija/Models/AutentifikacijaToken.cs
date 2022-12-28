using StudentOglasi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentOglasi.Autentifikacija.Models
{
    public class AutentifikacijaToken
    {
        [Key]
        public int id { get; set; }
        public string vrijednost { get; set; }
        [ForeignKey("korisnik")]
        public int KorisnikId { get; set; }
        public Korisnik korisnik { get; set; }
        public DateTime vrijemeEvidentiranja { get; set; }
    }
}
