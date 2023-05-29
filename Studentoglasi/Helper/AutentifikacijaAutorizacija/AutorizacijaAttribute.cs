using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using StudentOglasi.Models;

namespace StudentOglasi.Helper.AutentifikacijaAutorizacija
{
    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool Student, bool ReferentFakulteta, bool ReferentUniverzitteta, bool IzdavacSmjestaja, bool UposlenikFirme, bool UposlenikStipenditora, bool Admin)
        : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { Student, ReferentFakulteta, ReferentUniverzitteta, IzdavacSmjestaja, UposlenikFirme, UposlenikStipenditora, Admin };
        }
    }
    public class MyAuthorizeImpl : IActionFilter
    {
        private readonly bool _studenti;
        private readonly bool _referenti_fakulteta;
        private readonly bool _referenti_univerziteta;
        private readonly bool _izdavaci_smjestaja;
        private readonly bool _uposlenici_firme;
        private readonly bool _uposlenici_stipenditora;
        private readonly bool _admini;

        public MyAuthorizeImpl(bool student, bool prodreferent_fakulteta, bool referent_univerziteta, bool izdavac_smjestaja, bool uposlenik_firme, bool uposlenik_stipenditora, bool admin)
        {
            _studenti= student;
       _referenti_fakulteta= prodreferent_fakulteta;
         _referenti_univerziteta= referent_univerziteta;
        _izdavaci_smjestaja= izdavac_smjestaja;
       _uposlenici_firme= uposlenik_firme;
        _uposlenici_stipenditora= uposlenik_stipenditora;
         _admini=admin;
    }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            KretanjePoSistemu.Save(filterContext.HttpContext);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            AuthToken.LoginInformacije loginInfo = filterContext.HttpContext.GetLoginInfo();
            if (!loginInfo.isLogiran || loginInfo.korisnik == null)
            {
                filterContext.Result = new UnauthorizedResult();
                return;
            }

            //if (!loginInfo.korisnik.isAktiviran)
            //{
            //    filterContext.Result = new UnauthorizedObjectResult("korisnik nije aktiviran - provjerite email poruke " + loginInfo.korisnik.Email);
            //    return;
            //}


            if (loginInfo.korisnik.isAdmin)
            {
                if (loginInfo.autentifikacijaToken == null || !loginInfo.autentifikacijaToken.twoFactorOtkljucano)
                {
                    filterContext.Result = new UnauthorizedObjectResult("potrebno je otkljucati login sa codom poslat na email " + loginInfo.korisnik.Email);
                    return;
                }

                return;//ok - ima pravo pristupa
            }

        
            if (loginInfo.korisnik.isStudent && _studenti)
            {
                if (loginInfo.autentifikacijaToken == null || !loginInfo.autentifikacijaToken.twoFactorOtkljucano)
                {
                    filterContext.Result = new UnauthorizedObjectResult("potrebno je otkljucati login sa codom poslat na email " + loginInfo.korisnik.Email);
                    return;
                }

                return;//ok - ima pravo pristupa
               
            }

            if (loginInfo.korisnik.isReferentFakulteta && _referenti_fakulteta)
            {
                if (loginInfo.autentifikacijaToken == null || !loginInfo.autentifikacijaToken.twoFactorOtkljucano)
                {
                    filterContext.Result = new UnauthorizedObjectResult("potrebno je otkljucati login sa codom poslat na email " + loginInfo.korisnik.Email);
                    return;
                }
                return;//ok - ima pravo pristupa
            }

            if (loginInfo.korisnik.isReferentUniverziteta && _referenti_univerziteta)
            {
                if (loginInfo.autentifikacijaToken == null || !loginInfo.autentifikacijaToken.twoFactorOtkljucano)
                {
                    filterContext.Result = new UnauthorizedObjectResult("potrebno je otkljucati login sa codom poslat na email " + loginInfo.korisnik.Email);
                    return;
                }
                return;//ok - ima pravo pristupa
            }
            if (loginInfo.korisnik.isUposlenikFirme && _uposlenici_firme )
            {
                if (loginInfo.autentifikacijaToken == null || !loginInfo.autentifikacijaToken.twoFactorOtkljucano)
                {
                    filterContext.Result = new UnauthorizedObjectResult("potrebno je otkljucati login sa codom poslat na email " + loginInfo.korisnik.Email);
                    return;
                }
                return;//ok - ima pravo pristupa
            }
            if (loginInfo.korisnik.isUposlenikStipenditora && _uposlenici_stipenditora)
            {
                if (loginInfo.autentifikacijaToken == null || !loginInfo.autentifikacijaToken.twoFactorOtkljucano)
                {
                    filterContext.Result = new UnauthorizedObjectResult("potrebno je otkljucati login sa codom poslat na email " + loginInfo.korisnik.Email);
                    return;
                }
                return;//ok - ima pravo pristupa
            }
            if (loginInfo.korisnik.isIzdavacSmjestaja && _izdavaci_smjestaja)
            {
                if (loginInfo.autentifikacijaToken == null || !loginInfo.autentifikacijaToken.twoFactorOtkljucano)
                {
                    filterContext.Result = new UnauthorizedObjectResult("potrebno je otkljucati login sa codom poslat na email " + loginInfo.korisnik.Email);
                    return;
                }
                return;//ok - ima pravo pristupa
            }

            //else nema pravo pristupa
            filterContext.Result = new UnauthorizedResult();
        }
    }
}
