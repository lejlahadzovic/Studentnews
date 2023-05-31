import { Component, OnInit } from '@angular/core';
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {MojConfig} from "../MojConfig";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-pregled-rezervacija',
  templateUrl: './pregled-rezervacija.component.html',
  styleUrls: ['./pregled-rezervacija.component.css']
})
export class PregledRezervacijaComponent implements OnInit {
  logiraniKorisnik: any;
  rezervacije: any;
  displayedColumns: string[] = ['brojIndeksa', 'student', 'datumPrijave','datumOdjave','brojOsoba'];

  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
    this.logiraniKorisnik=AutentifikacijaHelper.getLoginInfo().autentifikacijaToken?.korisnik;
    this.getRezervacije();
  }

  private getRezervacije() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/RezervacijaSmjestaja/GetByIzdavacId?izdavacID="+this.logiraniKorisnik.id).subscribe(((x: any) => {
      this.rezervacije = x;
    }));
  }
}
