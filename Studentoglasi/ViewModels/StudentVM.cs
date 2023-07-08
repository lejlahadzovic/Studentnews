namespace StudentOglasi.ViewModels
{
    public class StudentVM
    {
        public int id { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string broj_indeksa { get; set; }
        public int godinaStudija { get; set; }
        public string nacin_studiranja { get; set; }
        public int fakultetID { get; set; }
        public string? naziv_fakulteta { get; set; }
        public IFormFile? slika { get; set; }
    }
}
