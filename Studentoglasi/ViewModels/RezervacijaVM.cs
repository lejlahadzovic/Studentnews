namespace StudentOglasi.ViewModels
{
    public class RezervacijaVM
    {
        public int ID { get; set; }
        public int SmjestajId { get; set; }
        public int StudentId { get; set; }
        public DateTime DatumPrijave { get; set; }
        public DateTime DatumOdjave { get; set; }
        public int BrojOsoba { get; set; }
    }
}
