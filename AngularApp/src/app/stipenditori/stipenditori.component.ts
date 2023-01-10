import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";
import {MojConfig} from "../MojConfig";

@Component({
  selector: 'app-stipenditori',
  templateUrl: './stipenditori.component.html',
  styleUrls: ['./stipenditori.component.css']
})
export class StipenditoriComponent implements OnInit {

  podaciStipenditori: any;
  title: string="StipenditorComponent" ;
  displayedColumns: string[] = ['naziv','adresa', 'email', 'tipUstanove', 'veza','grad','akcije'];
  odabraniStipenditor:any;
  dialogtitle: any;
  stipenditor:any;
  gradoviPodaci:any;
  constructor(private httpKlijent: HttpClient,private dialog: MatDialog) {

  }
  dodajStipenditora() {
    this.dialogtitle='Dodaj Stipenditora';

    this.stipenditor={
      id:0,
      naziv:'',
      adresa:'',
      email:'',
      tipUstanove:'',
      veza:'',
      gradID:0,

    }
  }

  urediStipenditora(x:any) {
    this.dialogtitle='Uredi stipenditora';
    this.stipenditor=x;

  }
  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'20%'
    });
  }
  obrisiStipenditora(s: any) {

    this.httpKlijent.post(`${MojConfig.adresa_servera}/Stipenditor/Obrisi/${s.id}`, MojConfig.http_opcije()).subscribe(x=>{
      this.preuzmiPodatke();
    });
  }
  private getGradovi() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Grad/GetAll").subscribe(((x: any) => {
      this.gradoviPodaci = x;
    }));
  }


  snimi_dugme() {

    this.httpKlijent.post(`${MojConfig.adresa_servera}/Stipenditor/Snimi`, this.stipenditor, MojConfig.http_opcije()).subscribe(x=>{
      this.preuzmiPodatke();
      this.stipenditor=null;
    });
  }
  preuzmiPodatke() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Stipenditor/GetAll").subscribe(((x: any) => {
      this.podaciStipenditori = x;
    }));
  }

  ngOnInit(): void {
    this.preuzmiPodatke();
    this.getGradovi();
  }


}
