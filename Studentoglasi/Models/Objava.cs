using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentOglasi.Models
{
    public class Objava
    {
        [Key]
        public int ID { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime VrijemeObjave { get; set; }
        public string Slika { get; set; }
        [ForeignKey("KategorijaID")]
        public Kategorija Kategorija { get; set; }
        public int KategorijaID { get; set; }
    }
}
