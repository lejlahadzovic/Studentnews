using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using StudentOglasi.Autentifikacija.Models;
using StudentOglasi.Data;
using StudentOglasi.Models;

namespace StudentOglasi.Helper.AutentifikacijaAutorizacija
{
    public class KretanjePoSistemu
    {
        public static int Save(HttpContext httpContext, IExceptionHandlerPathFeature? exceptionMessage = null)
        {
            Korisnik? korisnik = httpContext.GetLoginInfo().korisnik;

            var request = httpContext.Request;

            var queryString = request.Query;

          
            string detalji = "";
            if (request.HasFormContentType)
            {
                foreach (string key in request.Form.Keys)
                {
                    detalji += " | " + key + "=" + request.Form[key];
                }
            }

            var x = new LogKretanjePoSistemu
            {
                korisnik = korisnik,
                vrijeme = DateTime.Now,
                queryPath = request.GetEncodedPathAndQuery(),
                postData = detalji,
                ipAdresa = request.HttpContext.Connection.RemoteIpAddress?.ToString(),
            };

            if (exceptionMessage != null)
            {
                x.isException = true;
                x.exceptionMessage = exceptionMessage.Error.Message + " |" + exceptionMessage.Error.InnerException;
            }

            AppDbContext  db = httpContext.RequestServices.GetService<AppDbContext>();

            db.Add(x);
            db.SaveChanges();

            return x.id;
        }




    }
}