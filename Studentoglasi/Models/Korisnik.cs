using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentOglasi.Models
{
    public abstract class Korisnik
    {
        [Key]
        public int ID { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string? Slika { get; set; }

        [JsonIgnore]
        public Student student => this as Student;
        [JsonIgnore]
        public IzdavacSmjestaja izdavac => this as IzdavacSmjestaja;
        [JsonIgnore]
        public Administrator admin => this as Administrator;
        [JsonIgnore]
        public ReferentFakulteta referentFakulteta => this as ReferentFakulteta;
        [JsonIgnore]
        public ReferentUniverziteta referentUniverziteta => this as ReferentUniverziteta;
        [JsonIgnore]
        public UposlenikFirme uposlenikFirme => this as UposlenikFirme;
        [JsonIgnore]
        public UposlenikStipenditora uposlenikStipenditora => this as UposlenikStipenditora;

        public bool isStudent=>student!= null;
        public bool isAdmin => admin != null;
        public bool isReferentFakulteta => referentFakulteta != null;
        public bool isReferentUniverziteta => referentUniverziteta != null;
        public bool isIzdavacSmjestaja => izdavac != null;
        public bool isUposlenikFirme => uposlenikFirme != null;
        public bool isUposlenikStipenditora => uposlenikStipenditora != null;

    }
}
