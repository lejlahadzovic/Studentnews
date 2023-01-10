using StudentOglasi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentOglasi.Controllers
{
    public class KategorijaVM
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
    }
}