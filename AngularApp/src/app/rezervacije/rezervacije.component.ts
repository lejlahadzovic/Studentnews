import { Component, OnInit } from '@angular/core';
import {HttpClient,HttpParams} from "@angular/common/http";
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {MojConfig} from "../MojConfig";

@Component({
  selector: 'app-rezervacije',
  templateUrl: './rezervacije.component.html',
  styleUrls: ['./rezervacije.component.css']
})
export class RezervacijeComponent implements OnInit {
  rezervacije: any;
  displayedColumns: string[] = ['naslov', 'grad','cijena', 'datumPrijave','datumOdjave','brojOsoba','akcije'];
  logiraniKorisnik: any;

  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
    this.logiraniKorisnik=AutentifikacijaHelper.getLoginInfo().autentifikacijaToken?.korisnik;
    this.getRezervacije();
  }

  private getRezervacije() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/RezervacijaSmjestaja/GetByStudentId?studentID="+this.logiraniKorisnik.id).subscribe(((x: any) => {
      this.rezervacije = x;
    }));
  }

  otkaziRezervaciju(element:any) {
    const studentId = this.logiraniKorisnik.id;
    const smjestajId = element.smjestajId;

    const url = `${MojConfig.adresa_servera}/RezervacijaSmjestaja/Obrisi`;
    let params = new HttpParams();
    params = params.set('studentId', studentId.toString());
    params = params.set('smjestajId', smjestajId.toString());

    this.httpKlijent.post(url, {}, { params: params }).subscribe((s: any) => {
      this.getRezervacije();
    });
  }
}
