using System.ComponentModel.DataAnnotations.Schema;

namespace StudentOglasi.Models
{
    [Table("Praksa")]
    public class Praksa:Oglas
    {
        public DateTime Trajanje { get; set; }
        public string Kvalifikacije { get; set; }
        public string Benefiti { get; set; }
        public bool Placena { get; set; }
        [ForeignKey("UposlenikID")]
        public UposlenikFirme Uposlenik { get; set; }
        public int UposlenikID { get; set; }
        public List<PrijavaPraksa> Prijave { get; set; }
    }
}
