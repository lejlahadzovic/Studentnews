import {Component, OnInit, ViewChild} from '@angular/core';
import {MojConfig} from "../MojConfig";
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute} from "@angular/router";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {AppComponent} from "../app.component";

@Component({
  selector: 'app-stipendija-detalji',
  templateUrl: './stipendija-detalji.component.html',
  styleUrls: ['./stipendija-detalji.component.css'],
})
export class StipendijaDetaljiComponent implements OnInit {
  stipendijaId:number=0;
  stipendija: any;
  slikaPutanja=MojConfig.SlikePutanja;
  showCommentInput: boolean[] = [];
  komentar:string="";
  odgovor:string="";
  komentari: any;
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute, private appComponent:AppComponent) { }

  ngOnInit(): void {
    this.route.params.subscribe(params=>{
      this.stipendijaId= +params['id'];
      this.getStipenijaDetalji();
      this.getKomentari();
    })
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
  private getStipenijaDetalji() {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Stipendija/GetById?id="+this.stipendijaId).subscribe((x:any)=>{
      this.stipendija=x
    });
  }

  private getKomentari() {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Komentari/GetKomentari?oglasId="+this.stipendijaId).subscribe((x:any)=>{
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
        oglasId: this.stipendijaId,
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
        oglasId: this.stipendijaId,
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
