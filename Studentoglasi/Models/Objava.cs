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
        public ReferentFakulteta ReferentFakulteta { get; set; }
        public int? ReferentFakultetaID { get; set; }
        public ReferentUniverziteta ReferentUniverziteta { get; set; }
        public int? ReferentUniverzitetaID { get; set; }
        public UposlenikFirme UposlenikFirme { get; set; }
        public int? UposlenikFirmeID { get; set; }
        public UposlenikStipenditora UposlenikStipenditora { get; set; }
        public int? UposlenikStipenditoraID { get; set; }
    }
}
