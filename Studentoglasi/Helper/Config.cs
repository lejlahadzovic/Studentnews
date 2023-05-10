using System.Drawing;

namespace StudentOglasi.Helper
{
    public class Config
    {
        public static string SlikePutanja = "https://localhost:7296/Slike/";
        public static string CVPutanja = "https://localhost:7296/CV/";
        public static string DokumentacijaPutanja = "https://localhost:7296/Dokumentacija/";
        public static string ProsjekOcjenaPutanja = "https://localhost:7296/ProsjekOcjena/";

        public static string CertifikatiPutanja = "https://localhost:7296/Certifikati/";
        public static string PropratnoPismoPutanja = "https://localhost:7296/PropratnoPismo/";
        public static string Slike => "Slike/";
        public static string SlikeFolder => "wwwroot/" + Slike;
        public static string CV => "CV/";

        public static string CVFolder => "wwwroot/" + CV;

        public static string Dokumentacija => "Dokumentacija/";
        public static string DokumentacijaFolder => "wwwroot/" + Dokumentacija;

        public static string ProsjekOcjena => "ProsjekOcjena/";

        public static string ProsjekOcjenaFolder => "wwwroot/" + ProsjekOcjena;

        public static string Certifikati => "Certifikati/";
        public static string CertifikatiFolder => "wwwroot/" + Certifikati;

        public static string PropratnoPismo => "PropratnoPismo/";

        public static string PropratnoPismoFolder => "wwwroot/" + PropratnoPismo;

    }
}
