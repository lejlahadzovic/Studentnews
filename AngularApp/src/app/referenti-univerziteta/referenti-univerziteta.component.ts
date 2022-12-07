import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../MojConfig";
import {HttpClient} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";

@Component({
  selector: 'app-referenti-univerziteta',
  templateUrl: './referenti-univerziteta.component.html',
  styleUrls: ['./referenti-univerziteta.component.css']
})
export class ReferentiUniverzitetaComponent implements OnInit {
  referentiPodaci: any;
  referent:any;
  displayedColumns: string[] = ['ime', 'prezime','email', 'ustanova','akcije'];
  dialogtitle: any;
  filter_imePrezime= '';
  univerzitetiPodaci: any;
  ustanova:any;

  podaci: { ime: string;prezime: string ;email: string; naziv_univerziteta: string }[] = [];

  constructor(private httpKlijent:HttpClient,private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getReferenti();
    this.getUniverziteti();
    this.getpodaci1();
  }

  getReferenti() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/ReferentUniverziteta/GetAll").subscribe(((x: any) => {
      this.referentiPodaci = x
    }));
  }

  getUniverziteti() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Univerzitet/GetAll").subscribe(((x: any) => {
      this.univerzitetiPodaci = x;
    }));
  }

  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'40%'
    });
  }

  getpodaci(){
    if(this.referentiPodaci==null)
      return [];
    return this.referentiPodaci.filter((x:any)=>
      (x.ime+' '+x.prezime).startsWith(this.filter_imePrezime)||
      (x.prezime+' '+x.ime).startsWith(this.filter_imePrezime)
    );
  }

  dodajReferenta() {
    this.dialogtitle='Dodaj referenta';
    this.referent={
      id:0,
      password:'',
      username:'',
      ime:'',
      prezime:'',
      email:'',
      univerzitet_id:1
    }
  }
  snimiRefUnierzitet() {
    this.httpKlijent.post(MojConfig.adresa_servera+"/ReferentUniverziteta/Snimi",this.referent).subscribe((x:any)=>{
      this.getReferenti();
    })
  }

  editReferenta(x:any) {
    this.dialogtitle='Edit referenta';
    this.referent=x;
  }
  obrisiReferenta(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+"/ReferentUniverziteta/Obrisi",x).subscribe((x:any)=>{
      this.getReferenti();
    })
  }

  validacija() {
    return this.referent.password==''||
      this.referent.username==''||
      this.referent.ime==''||
      this.referent.prezime==''||
      this.referent.email=='';
  }

  snimi() {
      this.snimiRefUnierzitet();
  }

  getpodaci1(){
    if(this.referentiPodaci==null)
      return [];
    for (var i=0, length = this.referentiPodaci.length; i<length; i++){
      this.podaci.push(this.referentiPodaci[i]);
    }
    return this.podaci;
  }
}
