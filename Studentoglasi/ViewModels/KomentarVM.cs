namespace StudentOglasi.ViewModels
{
    public class KomentarVM
    {
        public int? objavaId { get; set; }
        public int? oglasId { get; set; }
        public int? komentarId { get; set; }
        public int korisnikId { get; set; }
        public string text { get; set; }
        public DateTime? vrijemeObjave { get; set; }
    }
}
