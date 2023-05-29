using System.ComponentModel.DataAnnotations.Schema;

namespace StudentOglasi.Models
{
    [Table("Administrator")]
    public class Administrator:Korisnik
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
    }
}
