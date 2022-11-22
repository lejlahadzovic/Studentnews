using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentOglasi.Models
{
    public class Komentar
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("ObajvaID")]
        public Objava Objava { get; set; }
        public int ObajvaID { get; set; }
        [ForeignKey("KorisnikID")]
        public Korisnik Korisnik { get; set; }
        public int KorisnikID { get; set; }
        public DateTime VrijemeObjave { get; set; }
        public string Text { get; set; }
    }
}
