using System.ComponentModel.DataAnnotations.Schema;

namespace StudentOglasi.Models
{
    [Table("ReferentFakulteta")]
    public class ReferentFakulteta:Korisnik
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        [ForeignKey("FakultetID")]
        public Fakultet Fakultet { get; set; }
        public int FakultetID { get; set; }
        public List<Objava> Objave { get; set; }
    }
}
