using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentOglasi.Models
{
    [Table("UposlenikStipenditora")]
    public class UposlenikStipenditora:Korisnik
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        [ForeignKey("StipenditorID")]
        public Stipenditor Stipenditor { get; set; }
        public int StipenditorID { get; set; }
        public List<Objava> Objave { get; set; }
    }
}
