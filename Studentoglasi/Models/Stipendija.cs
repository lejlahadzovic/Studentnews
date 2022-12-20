using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentOglasi.Models
{
    [Table("Stipendija")]
    public class Stipendija:Oglas
    {
        public string Uslovi { get; set; }
        public double Iznos { get; set; }
        public string Kriterij { get; set; }
        public string PotrebnaDokumentacija { get; set; }
        public string Izvor { get; set; }
        public string NivoObrazovanja { get; set; }
        public int BrojStipendisata { get; set; }
        [ForeignKey("UposlenikID")]
        public UposlenikStipenditora Uposlenik { get; set; }
        public int UposlenikID { get; set; }
        public List<PrijavaStipendija> Prijave { get; set; }
    }
}
