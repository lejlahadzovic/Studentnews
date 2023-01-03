namespace StudentOglasi.ViewModels
{
    public class IzdavacSmjestajaVM
    {
        public int id { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string email { get; set; }
        public string broj_telefona { get; set; }
        public IFormFile? slika { get; set; }
    }
}
