using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentOglasi.Models
{
    public class Ocjena
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        public int StudentId { get; set; }
        public int Vrijednost { get; set; }
        public string? Komentar { get; set; }
    }
}
