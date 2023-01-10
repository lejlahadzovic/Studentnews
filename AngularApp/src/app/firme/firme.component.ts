import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";
import {MojConfig} from "../MojConfig";

@Component({
  selector: 'app-firme',
  templateUrl: './firme.component.html',
  styleUrls: ['./firme.component.css']
})
export class FirmeComponent implements OnInit {

  podaciFirme: any;
  title: string="FirmaComponent" ;
  displayedColumns: string[] = ['naziv','adresa', 'email', 'telefon', 'veza','grad','akcije'];
  odabraniFirma:any;
  dialogtitle: any;
  firma:any;
  gradoviPodaci:any;
  constructor(private httpKlijent: HttpClient,private dialog: MatDialog) {

  }
  dodajFirma() {
    this.dialogtitle='Dodaj Firma';

    this.firma={
      id:0,
      naziv:'',
      adresa:'',
      email:'',
      telefon:'',
      veza:'',
      gradID:0,

    }
  }

  urediFirma(x:any) {
    this.dialogtitle='Uredi Firma';
    this.firma=x;

  }
  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'20%'
    });
  }
  obrisiFirma(s: any) {

    this.httpKlijent.post(`${MojConfig.adresa_servera}/Firma/Obrisi/${s.id}`, MojConfig.http_opcije()).subscribe(x=>{
      this.preuzmiPodatke();
    });
  }
  private getGradovi() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Grad/GetAll").subscribe(((x: any) => {
      this.gradoviPodaci = x;
    }));
  }


  snimi_dugme() {

    this.httpKlijent.post(`${MojConfig.adresa_servera}/Firma/Snimi`, this.firma, MojConfig.http_opcije()).subscribe(x=>{
      this.preuzmiPodatke();
      this.firma=null;
    });
  }
  preuzmiPodatke() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Firma/GetAll").subscribe(((x: any) => {
      this.podaciFirme = x;
    }));
  }

  ngOnInit(): void {
    this.preuzmiPodatke();
    this.getGradovi();
  }


}
