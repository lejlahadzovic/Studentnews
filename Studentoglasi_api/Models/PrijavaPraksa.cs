namespace StudentOglasi.Models
{
    public class PrijavaPraksa
    {
        public Student Student { get; set; }
        public int StudentId { get; set; }
        public Praksa Praksa { get; set; }
        public int PraksaId { get; set; }
        public string PropratnoPismo { get; set; }
        public string CV { get; set; }
        public string Certifikati { get; set; }
    }
}
