namespace StudentOglasi.ViewModels
{
    public class ObjavaVM
    {
        public int id { get; set; }
        public string naslov { get; set; }
        public string sadrzaj { get; set; }
        public string? vrijemeObjave { get; set; }
        public IFormFile? slika { get; set; }
        public int kategorijaID { get; set; }
        public string? naziv_kategorije { get; set; }
    }
}
