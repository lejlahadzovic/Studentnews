import {Component, Input, OnInit} from '@angular/core';
import {MojConfig} from "../MojConfig";
import {AppComponent} from "../app.component";
import {HttpClient} from "@angular/common/http";
import {LoginInformacije} from "../helper/login-informacije";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";

@Component({
  selector: 'app-komentari',
  templateUrl: './komentari.component.html',
  styleUrls: ['./komentari.component.css']
})
export class KomentariComponent implements OnInit {

  komentari:any;
  @Input() oglasId:number=0;
  @Input() objavaId:number=0;
  slikaPutanja=MojConfig.SlikePutanja;
  showCommentInput: boolean[] = [];
  komentar:string="";
  odgovor:string="";

  constructor(private httpKlijent: HttpClient, private appComponent:AppComponent) { }

  ngOnInit(): void {
    this.getKomentari();
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
  prikaziCommentInput(i: number) {
    this.showCommentInput[i] = !this.showCommentInput[i];
  }
  openDialog() {
    this.appComponent.openDialog()
  }
  private getKomentari() {
    if(this.oglasId>0)
    this.httpKlijent.get(MojConfig.adresa_servera+"/Komentari/GetKomentari?oglasId="+this.oglasId).subscribe((x:any)=>{
      this.komentari=x
    });
    else if(this.objavaId>0)
      this.httpKlijent.get(MojConfig.adresa_servera+"/Komentari/GetKomentari?objavaId="+this.objavaId).subscribe((x:any)=>{
        this.komentari=x
      });
  }
  snimiKomentar() {
    if (!this.loginInfo().isLogiran) {
      this.openDialog();
    } else {
      const podaci = {
        oglasId: this.oglasId,
        objavaId:this.objavaId,
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
        oglasId: this.oglasId,
        objavaId:this.objavaId,
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
