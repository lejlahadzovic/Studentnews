import { Component } from '@angular/core';
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

  ngOnInit(): void {
  }
  constructor(private httpKlijent: HttpClient, private router: Router, private dialog: MatDialog) {
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  logout() {
    AutentifikacijaHelper.setLoginInfo(null);

    this.httpKlijent.post(MojConfig.adresa_servera + "/Autentifikacija/Logout/", null, MojConfig.http_opcije())
      .subscribe((x: any) => {
      });
  }

  login() {
    this.router.navigateByUrl("/login");
  }

  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'25%'
    });
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
          this.router.navigateByUrl("/putanja-smjestaji");

        }
        else
        {
          AutentifikacijaHelper.setLoginInfo(null);
        }
      });
  }
}
