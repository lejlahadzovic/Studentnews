namespace StudentOglasi.ViewModels
{
    public class UposlenikFirmeVM
    {
        public int id { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string email { get; set; }
        public string pozicija { get; set; }
        public int firmaID { get; set; }
        public string? naziv_firme { get; set; }
        public IFormFile? slika { get; set; }
    }
}
