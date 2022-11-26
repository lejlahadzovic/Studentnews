using System.ComponentModel.DataAnnotations.Schema;

namespace StudentOglasi.Models
{
    [Table("IzdavacSmjestaja")]
    public class IzdavacSmjestaja:Korisnik
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
    }
}
