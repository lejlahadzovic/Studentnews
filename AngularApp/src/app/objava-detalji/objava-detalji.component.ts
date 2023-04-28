import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute} from "@angular/router";
import {MojConfig} from "../MojConfig";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {AppComponent} from "../app.component";

@Component({
  selector: 'app-objava-detalji',
  templateUrl: './objava-detalji.component.html',
  styleUrls: ['./objava-detalji.component.css']
})
export class ObjavaDetaljiComponent implements OnInit {
  objavaId:number=0;
  objava: any;
  komentari: any;
  slikaPutanja=MojConfig.SlikePutanja;
  showCommentInput: boolean[] = [];
  komentar:string="";
  odgovor:string="";

  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute, private appComponent:AppComponent) { }

  ngOnInit(): void {
    this.route.params.subscribe(params=>{
      this.objavaId= +params['id'];
      this.getObjavaDetalji();
      this.getKomentari();
    })
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  private getObjavaDetalji() {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Objava/GetById?id="+this.objavaId).subscribe((x:any)=>{
      this.objava=x
    });
  }

  private getKomentari() {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Komentari/GetKomentari?objavaId="+this.objavaId).subscribe((x:any)=>{
      this.komentari=x
    });
  }

  prikaziCommentInput(i: number) {
    this.showCommentInput[i] = !this.showCommentInput[i];
  }

  openDialog() {
    this.appComponent.openDialog()
  }

  snimiKomentar() {
    if (!this.loginInfo().isLogiran) {
      this.openDialog();
    } else {
      const podaci = {
        objavaId: this.objavaId,
        korisnikId: this.loginInfo().autentifikacijaToken?.korisnik.id,
        text: this.komentar
      }
      this.httpKlijent.post(MojConfig.adresa_servera + "/Komentari/Snimi", podaci).subscribe((s: any) => {
        this.getKomentari();
        this.komentar = "";
      })
    }
  }

  snimiOdgovor(komentar: any) {
    if (!this.loginInfo().isLogiran) {
      this.openDialog();
    } else {
      const podaci = {
        objavaId: this.objavaId,
        korisnikId: this.loginInfo().autentifikacijaToken?.korisnik.id,
        komentarId: komentar.id,
        text: this.odgovor
      }
      this.httpKlijent.post(MojConfig.adresa_servera + "/Komentari/Snimi", podaci).subscribe((s: any) => {
        this.getKomentari();
        this.odgovor = "";
      })
    }
  }
}
