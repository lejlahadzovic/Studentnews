import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../MojConfig";
import {MatDialog} from "@angular/material/dialog";
import {formatDate} from "@angular/common";

@Component({
  selector: 'app-univerziteti',
  templateUrl: './univerziteti.component.html',
  styleUrls: ['./univerziteti.component.css']
})
export class UniverzitetiComponent implements OnInit {

  podaciUniveziteti: any;
  title: string="AppComponent" ;
  displayedColumns: string[] = ['naziv', 'email', 'telefon', 'veza','grad','akcije'];
odabraniUniverzitet:any;
  dialogtitle: any;
  univerzitet:any;
  gradoviPodaci:any;
  constructor(private httpKlijent: HttpClient,private dialog: MatDialog) {

  }
  dodajUniverzitet() {
    this.dialogtitle='Dodaj univerzitet';

    this.univerzitet={
      id:0,
      naziv:'',
      email:'',
      telefon:'',
      veza:'',
      gradID:0,

    }
  }

  urediUniverzitet(x:any) {
    this.dialogtitle='Uredi univerzitet';
    this.univerzitet=x;

  }
  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
     width:'20%'
    });
  }
  obrisiUniverzitet(s: any) {

    this.httpKlijent.post(`${MojConfig.adresa_servera}/Student/Obrisi2/${s.id}`, MojConfig.http_opcije()).subscribe(x=>{
      this.preuzmiPodatke();
    });
  }
  private getGradovi() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/F/GetGradovi").subscribe(((x: any) => {
      this.gradoviPodaci = x;
    }));
  }


  snimi_dugme() {

    this.httpKlijent.post(`${MojConfig.adresa_servera}/Univerzitet/Snimi`, this.univerzitet, MojConfig.http_opcije()).subscribe(x=>{
      this.preuzmiPodatke();
      this.univerzitet=null;
    });
  }
  preuzmiPodatke() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Univerzitet/GetAll").subscribe(((x: any) => {
      this.podaciUniveziteti = x;
    }));
  }

  ngOnInit(): void {
    this.preuzmiPodatke();
    this.getGradovi();
  }


}
