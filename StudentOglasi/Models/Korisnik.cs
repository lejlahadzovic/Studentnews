using System.ComponentModel.DataAnnotations;

namespace StudentOglasi.Models
{
    public abstract class Korisnik
    {
        [Key]
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
