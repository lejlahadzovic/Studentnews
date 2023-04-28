using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentOglasi.Models
{
    public class Komentar
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("ObjavaID")]
        public Objava? Objava { get; set; }
        public int? ObjavaID { get; set; }
        [ForeignKey("OglasID")]
        public Oglas? Oglas { get; set; }
        public int? OglasID { get; set; }
        [ForeignKey("KomentarID")]
        public Komentar? komentar { get; set; }
        public int? KomentarID { get; set; }
        [ForeignKey("KorisnikID")]
        public Korisnik Korisnik { get; set; }
        public int KorisnikID { get; set; }
        public DateTime VrijemeObjave { get; set; }
        public string Text { get; set; }
    }
}
