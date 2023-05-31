import { Component, OnInit } from '@angular/core';
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {MojConfig} from "../MojConfig";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-prijave-praksa',
  templateUrl: './prijave-praksa.component.html',
  styleUrls: ['./prijave-praksa.component.css']
})
export class PrijavePraksaComponent implements OnInit {
  logiraniKorisnik: any;
  prijave: any;
  displayedColumns: string[] = ['brojIndeksa', 'student', 'propratnoPismo','cv','certifikati'];

  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
    this.logiraniKorisnik=AutentifikacijaHelper.getLoginInfo().autentifikacijaToken?.korisnik;
    this.getPrijave();
  }

  private getPrijave() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/PrijavaPraksa/GetByUposlenikId?uposlenikID="+this.logiraniKorisnik.id).subscribe(((x: any) => {
      this.prijave = x;
    }));
  }
}
