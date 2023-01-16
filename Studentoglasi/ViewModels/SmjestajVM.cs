namespace StudentOglasi.ViewModels
{
    public class SmjestajVM
    {
        public int id { get; set; }
        public string naslov { get; set; }
        public DateTime rokPrijave { get; set; }
        public string opis { get; set; }
        public IFormFile? slika { get; set; }
        public double cijena { get; set; }
        public int kapacitet { get; set; }
        public string dodatneUsluge { get; set; }
        public int brojSoba { get; set; }
        public bool parking { get; set; }
        public string nacinGrijanja { get; set; }
        public int gradID { get; set; }
        public int izdavacID { get; set; }

    }
}
