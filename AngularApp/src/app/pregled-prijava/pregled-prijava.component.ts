import { Component, OnInit } from '@angular/core';
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {MojConfig} from "../MojConfig";
import {HttpClient, HttpParams} from "@angular/common/http";

@Component({
  selector: 'app-pregled-prijava',
  templateUrl: './pregled-prijava.component.html',
  styleUrls: ['./pregled-prijava.component.css']
})
export class PregledPrijavaComponent implements OnInit {
  logiraniKorisnik: any;
  stipendijaPrijave: any;
  displayedColumnsStipendija: string[] = ['naslov', 'rokPrijave','iznos', 'dokumentacija','cv','prosjekOcjena','akcije'];
  displayedColumnsPraksa: string[] = ['naslov', 'trajanjePrakse','placena', 'propratnoPismo','cv','certifikati','akcije'];
  praksaPrijave: any;

  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
    this.logiraniKorisnik=AutentifikacijaHelper.getLoginInfo().autentifikacijaToken?.korisnik;
    this.getStipendijaPrijave();
    this.getPraksaPrijave();
  }

  private getStipendijaPrijave() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/PrijavaStipendija/GetByStudentId?studentID="+this.logiraniKorisnik.id).subscribe(((x: any) => {
      this.stipendijaPrijave = x;
    }));
  }

  private getPraksaPrijave() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/PrijavaPraksa/GetByStudentId?studentID="+this.logiraniKorisnik.id).subscribe(((x: any) => {
      this.praksaPrijave = x;
    }));
  }

  otkaziPrijavuStipendija(element :any) {
    const studentId = this.logiraniKorisnik.id;
    const stipendijaId = element.stipendijaId;

    const url = `${MojConfig.adresa_servera}/PrijavaStipendija/Obrisi`;
    let params = new HttpParams();
    params = params.set('studentId', studentId.toString());
    params = params.set('stipendijaId', stipendijaId.toString());

    this.httpKlijent.post(url, {}, { params: params }).subscribe((s: any) => {
      this.getStipendijaPrijave();
    });
  }
  otkaziPrijavuPraksa(element :any) {
    const studentId = this.logiraniKorisnik.id;
    const praksaId = element.praksaId;

    const url = `${MojConfig.adresa_servera}/PrijavaPraksa/Obrisi`;
    let params = new HttpParams();
    params = params.set('studentId', studentId.toString());
    params = params.set('praksaId', praksaId.toString());

    this.httpKlijent.post(url, {}, { params: params }).subscribe((s: any) => {
      this.getPraksaPrijave();
    });
  }
}
