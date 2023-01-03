namespace StudentOglasi.ViewModels
{
    public class ReferentUniverzitetaVM
    {
        public int id { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string email { get; set; }
        public int univerzitetID { get; set; }
        public string? naziv_univerziteta { get; set; }
        public IFormFile? slika { get; set; }
    }
}
