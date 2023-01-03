namespace StudentOglasi.ViewModels
{
    public class UposlenikStipenditoraVM
    {
        public int id { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string email { get; set; }
        public int stipenditorID { get; set; }
        public string? naziv_stipenditora { get; set; }
        public IFormFile? slika { get; set; }
    }
}
