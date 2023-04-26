using Microsoft.OpenApi.Any;
using StudentOglasi.Models;

namespace StudentOglasi.Helper
{
    public class DodajFile
    {
        public static string AddFileStipendija(string naziv, PrijavaStipendija prijavaStipendija, IFormFile? x)
        {


            string ekstenzija = Path.GetExtension(x.FileName);

            var filename = $"{Guid.NewGuid()}{ekstenzija}";

            var myFile = new FileStream(naziv + filename, FileMode.Create);
            x.CopyTo(myFile);

            myFile.Close();
            return filename;


        }
        public static string AddFilePraksa(string naziv, PrijavaPraksa prijavaStipendija, IFormFile? x)
        {


            string ekstenzija = Path.GetExtension(x.FileName);

            var filename = $"{Guid.NewGuid()}{ekstenzija}";

            var myFile = new FileStream(naziv + filename, FileMode.Create);
            x.CopyTo(myFile);

            myFile.Close();
            return filename;


        }
    }

}
