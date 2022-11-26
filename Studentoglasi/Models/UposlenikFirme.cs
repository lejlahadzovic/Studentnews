using System.ComponentModel.DataAnnotations.Schema;

namespace StudentOglasi.Models
{
    [Table("UposlenikFirme")]
    public class UposlenikFirme:Korisnik
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Pozicija { get; set; }
        public string Email { get; set; }
        [ForeignKey("FirmaID")]
        public Firma Firma { get; set; }
        public int FirmaID { get; set; }
        public List<Objava> Objave { get; set; }
    }
}
