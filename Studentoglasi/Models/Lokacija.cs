using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentOglasi.Models
{
    public class Lokacija
    {
        [Key]
        public int ID { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
