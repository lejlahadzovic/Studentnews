using System.ComponentModel.DataAnnotations.Schema;

namespace StudentOglasi.Models
{
    [Table("ReferentUniverziteta")]
    public class ReferentUniverziteta:Korisnik
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        [ForeignKey("UnivetzitetID")]
        public Univerzitet Univerzitet { get; set; }
        public int UnivetzitetID { get; set; }
        public List<Objava> Objave { get; set; }
    }
}
