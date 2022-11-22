using System.Net.Mail;

namespace StudentOglasi.Models
{
    public class Rezervacija
    {
        public Smjestaj Smjestaj { get; set; }
        public int SmjestajId { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }
        public DateTime DatumPrijave { get; set; }
        public DateTime DatumOdjave { get; set; }
        public int BrojOsoba { get; set; }
    }
}
