import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../MojConfig";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {AppComponent} from "../app.component";

@Component({
  selector: 'app-praksa-detalji',
  templateUrl: './praksa-detalji.component.html',
  styleUrls: ['./praksa-detalji.component.css']
})
export class PraksaDetaljiComponent implements OnInit {
  praksaId:number=0;
  praksa: any;
  slikaPutanja=MojConfig.SlikePutanja;
  showCommentInput: boolean[] = [];
  komentar:string="";
  odgovor:string="";
  komentari: any;

  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute, private appComponent:AppComponent) { }

  ngOnInit(): void {
    this.route.params.subscribe(params=>{
      this.praksaId= +params['id'];
      this.getPraksaDetalji();
      this.getKomentari();
    })
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
  private getPraksaDetalji() {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Praksa/GetById?id="+this.praksaId).subscribe((x:any)=>{
      this.praksa=x
    });
  }

  private getKomentari() {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Komentari/GetKomentari?oglasId="+this.praksaId).subscribe((x:any)=>{
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
        oglasId: this.praksaId,
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
        oglasId: this.praksaId,
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
