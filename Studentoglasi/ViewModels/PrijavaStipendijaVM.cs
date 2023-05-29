using StudentOglasi.Models;

namespace StudentOglasi.ViewModels
{
    public class PrijavaStipendijaVM
    {
       
        public int StudentId { get; set; }
        public string studentIme{ get; set; }
        public int StipendijaID { get; set; }
        public IFormFile? Dokumentacija { get; set; }
        public IFormFile? CV { get; set; }
        public IFormFile? ProsjekOcjena { get; set; }
    }
}