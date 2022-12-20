namespace StudentOglasi.ViewModels
{
    public class StipendijaVM
    {
        public int id { get; set; }
        public string naslov { get; set; }
        public DateTime rokPrijave { get; set; }
        public string opis { get; set; }
        public IFormFile? slika { get; set; }
        public string uslovi { get; set; }
        public double iznos { get; set; }
        public string kriterij { get; set; }
        public string potrebnaDokumentacija { get; set; }
        public string izvor { get; set; }
        public string nivoObrazovanja { get; set; }
        public int brojStipendisata { get; set; }
        public int uposlenikID { get; set; }
    }
}
