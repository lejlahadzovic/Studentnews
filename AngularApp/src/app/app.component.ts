import {Component, Injectable, TemplateRef, ViewChild} from '@angular/core';
import {MojConfig} from "./MojConfig";
import {HttpClient} from "@angular/common/http";
import {LoginInformacije} from "./helper/login-informacije";
import {AutentifikacijaHelper} from "./helper/autentifikacija-helper";
import {keyframes} from "@angular/animations";
import {Router} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],

})
export class AppComponent{
  title: string="Upravljanje podacima";
  username: any;
  password:any;
  poruka: string='';
  korisnik:any;

  @ViewChild('dialogLogin') dialogLogin!: TemplateRef<any>;

  ngOnInit(): void {
    this.korisnik=this.loginInfo().autentifikacijaToken?.korisnik;
  }
  constructor(private httpKlijent: HttpClient, private router: Router, private dialog: MatDialog) {
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  logout() {

    this.httpKlijent.post(MojConfig.adresa_servera + "/Autentifikacija/Logout/", null, MojConfig.http_opcije())
      .subscribe((x: any) => {
        AutentifikacijaHelper.setLoginInfo(null);
      });
  }

  login() {
    this.router.navigateByUrl("/login");
  }

  openDialog() {
    this.dialog.open(this.dialogLogin, {
      width:'25%'
    });
    this.poruka='';
  }

  btnLogin() {
    let saljemo = {
      username:this.username,
      password: this.password
    };
    this.httpKlijent.post<LoginInformacije>(MojConfig.adresa_servera+ "/Autentifikacija/Login/", saljemo)
      .subscribe((x:LoginInformacije) =>{
        if (x.isLogiran) {
          AutentifikacijaHelper.setLoginInfo(x);
          this.dialog.closeAll();
          this.korisnik=this.loginInfo().autentifikacijaToken?.korisnik;
        }
        else
        {
          AutentifikacijaHelper.setLoginInfo(null);
          this.korisnik=null;
          this.username='';
          this.password='';
          this.poruka='Niste unijeli ispravno korisničko ime ili lozinku.';
        }
      });
  }

  registracija() {
    this.router.navigateByUrl("/registracija");
  }

  profil() {
    this.router.navigateByUrl("/profil");
  }

  objave() {
    if(this.korisnik.isIzdavacSmjestaja)
      this.router.navigateByUrl("/putanja-smjestaji");

    else if(this.korisnik.isReferentFakulteta||this.korisnik.isReferentUniverziteta)
      this.router.navigateByUrl("/putanja-objave");

    else if(this.korisnik.isUposlenikFirme)
      this.router.navigateByUrl("/putanja-prakse");

    else if(this.korisnik.isUposlenikStipenditora)
      this.router.navigateByUrl("/putanja-stipendije");
  }

  studenti() {
    this.router.navigateByUrl("/putanja-studenti");
  }
}
