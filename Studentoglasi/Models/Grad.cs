using System.ComponentModel.DataAnnotations;

namespace StudentOglasi.Models
{
    public class Grad
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
    }
}
