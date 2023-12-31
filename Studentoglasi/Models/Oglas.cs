﻿using System.ComponentModel.DataAnnotations;

namespace StudentOglasi.Models
{
    public abstract class Oglas
    {
        [Key]
        public int ID { get; set; }
        public string Naslov { get; set; }
        public DateTime RokPrijave { get; set; }
        public string Opis { get; set; }
        public DateTime VrijemeObjave { get; set; }
        public string Slika { get; set; }
    }
}
