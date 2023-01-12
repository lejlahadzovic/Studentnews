using StudentOglasi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentOglasi.Controllers
{
    public class StipenditorVM
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Email { get; set; }
        public string Veza { get; set; }
        public string TipUstanove { get; set; }
        public int GradID { get; set; }
    }
}