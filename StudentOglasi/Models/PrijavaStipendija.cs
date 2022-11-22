namespace StudentOglasi.Models
{
    public class PrijavaStipendija
    {
        public Student Student { get; set; }
        public int StudentId { get; set; }
        public Stipendija Stipendija { get; set; }
        public int StipendijaID { get; set; }
        public string Dokumentacija { get; set; }
        public string CV { get; set; }
        public double ProsjekOcjena { get; set; }
    }
}
