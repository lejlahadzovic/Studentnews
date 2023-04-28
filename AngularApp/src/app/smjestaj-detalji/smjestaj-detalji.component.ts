import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../MojConfig";
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute} from "@angular/router";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {AppComponent} from "../app.component";

@Component({
  selector: 'app-smjestaj-detalji',
  templateUrl: './smjestaj-detalji.component.html',
  styleUrls: ['./smjestaj-detalji.component.css']
})
export class SmjestajDetaljiComponent implements OnInit {
  smjestajId:number=0;
  smjestaj: any;
  slikaPutanja=MojConfig.SlikePutanja;
  showCommentInput: boolean[] = [];
  komentar:string="";
  odgovor:string="";
  komentari: any;

  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute, private appComponent:AppComponent) { }

  ngOnInit(): void {
    this.route.params.subscribe(params=>{
      this.smjestajId= +params['id'];
      this.getSmjestajDetalji();
      this.getKomentari();
    })
  }

  private getSmjestajDetalji() {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Smjestaj/GetById?id="+this.smjestajId).subscribe((x:any)=>{
      this.smjestaj=x
    });
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
  snimiOcjenu(data: any) {
    const newData= {
      ...data,
      smjestajID: this.smjestajId
    }
    this.httpKlijent.post(MojConfig.adresa_servera+"/Smjestaj/OcijeniSmjestaj",newData).subscribe((s:any)=>{
      location.reload();
    })
  }

  private getKomentari() {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Komentari/GetKomentari?oglasId="+this.smjestajId).subscribe((x:any)=>{
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
        oglasId: this.smjestajId,
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
        oglasId: this.smjestajId,
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
