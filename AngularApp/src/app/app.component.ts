import { Component, Injectable, TemplateRef, ViewChild, OnInit } from '@angular/core';
import {MojConfig} from "./MojConfig";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {LoginInformacije} from "./helper/login-informacije";
import {AutentifikacijaHelper} from "./helper/autentifikacija-helper";
import {keyframes} from "@angular/animations";
import {Router} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";
import { environment } from "../environments/environment";
import { getMessaging, getToken, onMessage } from "firebase/messaging";
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
@Injectable()
export class AppComponent implements OnInit{
  title: string="Upravljanje podacima";
  username: any;
  password:any;
  poruka: string='';
  korisnik:any;

  requestPermission() {
    const messaging = getMessaging();
    getToken(messaging,
      { vapidKey: environment.firebase.vapidKey}).then(
      (currentToken) => {
        if (currentToken) {
          console.log("Hurraaa!!! we got the token.....");
          console.log(currentToken);
        } else {
          console.log('No registration token available. Request permission to generate one.');
        }
      }).catch((err) => {
      console.log('An error occurred while retrieving token. ', err);
    });
  }
  listen() {
    const messaging = getMessaging();
    onMessage(messaging, (payload) => {
      console.log('Message received. ', payload);
      this.showNotification(payload.notification?.title,payload.notification?.body,payload.data?.['notificationType']);
    });
  }

  @ViewChild('dialogLogin') dialogLogin!: TemplateRef<any>;

  ngOnInit(): void {
    this.korisnik=this.loginInfo().autentifikacijaToken?.korisnik;
    this.requestPermission();
    this.listen();
  }
  constructor(private httpKlijent: HttpClient, private router: Router, private dialog: MatDialog,private toastr: ToastrService) {
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
          AutentifikacijaHelper.setLoginInfo(x)
          this.router.navigateByUrl("/two-f-otkljucaj");
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

  rezervacije() {
    this.router.navigateByUrl("/rezervacije");
  }

  prijave() {
    if(this.korisnik.isUposlenikFirme)
      this.router.navigateByUrl("/praksa-prijave");

    else if(this.korisnik.isUposlenikStipenditora)
      this.router.navigateByUrl("/stipendija-prijave");
  }

  pregledPrijava() {
    this.router.navigateByUrl("/prijave");
  }

  pregledRezervacija() {
    this.router.navigateByUrl("/moje-rezervacije");
  }

  private showNotification(title: string | undefined, body: string | undefined, notificationType: string | undefined ) {
    switch (notificationType) {
      case 'info':
        this.toastr.info(body, title);
        break;
      case 'warning':
        this.toastr.warning(body, title);
        break;
      case 'error':
        this.toastr.error(body, title);
        break;
      case 'success':
        this.toastr.success(body, title);
        break;
      default:
        this.toastr.info(body, title);
        break;
    }
  }
}
