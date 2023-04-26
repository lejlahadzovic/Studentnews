using StudentOglasi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public string twoFactorCode { get; set; }

        public bool twoFactorOtkljucano { get; set; }

    }
}
