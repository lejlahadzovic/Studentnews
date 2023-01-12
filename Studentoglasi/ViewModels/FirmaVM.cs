﻿using StudentOglasi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentOglasi.Controllers
{
    public class FirmaVM
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Adresa { get; set; }
        public string Veza { get; set; }
       
        public int GradID { get; set; }
    }
}