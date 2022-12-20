namespace StudentOglasi.ViewModels
{
    public class PraksaVM
    {
        public int id { get; set; }
        public string naslov { get; set; }
        public DateTime rokPrijave { get; set; }
        public string opis { get; set; }
        public IFormFile? slika { get; set; }
        public DateTime pocetakPrakse { get; set; }
        public DateTime krajPrakse { get; set; }
        public string kvalifikacije { get; set; }
        public string benefiti { get; set; }
        public bool placena { get; set; }
        public int uposlenikID { get; set; }
        public string? ime_uposlenika { get; set; }
        public string? naziv_firme { get; set; }
    }
}
