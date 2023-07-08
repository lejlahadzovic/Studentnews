using Microsoft.OpenApi.Any;
using StudentOglasi.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

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
        public static string UploadSlike(IFormFile slika, string staraSlika, IHostingEnvironment hostingEnvironment)
        {
            if (slika != null)
            {
                if (slika.Length > 500 * 1000)
                    throw new Exception("Maksimalna veličina fajla je 500 KB");

                if (!string.IsNullOrEmpty(staraSlika))
                {
                    string webRootPath = hostingEnvironment.WebRootPath;
                    var fullPath = Path.Combine(webRootPath + "/Slike/" + staraSlika);

                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }
                }

                string ekstenzija = Path.GetExtension(slika.FileName);
                string contentType = slika.ContentType;

                var filename = $"{Guid.NewGuid()}{ekstenzija}";

                var filePath = Path.Combine(Config.SlikeFolder, filename);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    slika.CopyTo(stream);
                }

                return filename;
            }

            return staraSlika;
        }
        public static void ObrisiSliku(string slika, IHostingEnvironment hostingEnvironment)
        {
            if (!string.IsNullOrEmpty(slika))
            {
                string webRootPath = hostingEnvironment.WebRootPath;
                var fullPath = Path.Combine(webRootPath, "Slike", slika);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
        }
    }

}
