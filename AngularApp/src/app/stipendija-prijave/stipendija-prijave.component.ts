import { Component, OnInit } from '@angular/core';
import {AutentifikacijaHelper} from "../helper/autentifikacija-helper";
import {MojConfig} from "../MojConfig";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-stipendija-prijave',
  templateUrl: './stipendija-prijave.component.html',
  styleUrls: ['./stipendija-prijave.component.css']
})
export class StipendijaPrijaveComponent implements OnInit {
  logiraniKorisnik: any;
  prijave: any;
  displayedColumns: string[] = ['brojIndeksa', 'student', 'dokumentacija','cv','prosjekOcjena'];

  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
    this.logiraniKorisnik=AutentifikacijaHelper.getLoginInfo().autentifikacijaToken?.korisnik;
    this.getPrijave();
  }

  private getPrijave() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/PrijavaStipendija/GetByUposlenikId?uposlenikID="+this.logiraniKorisnik.id).subscribe(((x: any) => {
      this.prijave = x;
    }));
  }
}
