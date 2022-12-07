import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";
import {MojConfig} from "../MojConfig";

@Component({
  selector: 'app-uposlenici-firme',
  templateUrl: './uposlenici-firme.component.html',
  styleUrls: ['./uposlenici-firme.component.css']
})
export class UposleniciFirmeComponent implements OnInit {
  uposleniciPodaci: any;
  uposlenik:any;
  displayedColumns: string[] = ['ime', 'prezime', 'email','firma', 'pozicija','akcije'];
  dialogtitle: any;
  filter_imePrezime= '';
  firmePodaci: any;

  constructor(private httpKlijent:HttpClient,private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getUposlenici();
    this.getFirme();
  }
  getUposlenici() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/UposlenikFirme/GetAll").subscribe(((x: any) => {
      this.uposleniciPodaci = x;
    }));
  }
  getFirme() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/F/GetAll").subscribe(((x: any) => {
      this.firmePodaci = x;
    }));
  }

  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'40%'
    });
  }

  getpodaci(){
    if(this.uposleniciPodaci==null)
      return [];
    return this.uposleniciPodaci.filter((s:any)=>
      (s.ime+' '+s.prezime).startsWith(this.filter_imePrezime)||
        (s.prezime+' '+s.ime).startsWith(this.filter_imePrezime)
    );
  }

  dodajUposlenika() {
    this.dialogtitle='Dodaj uposlenika';
    this.uposlenik={
      id:0,
      password:'',
      username:'',
      ime:'',
      prezime:'',
      email:'',
      pozicija: '',
      firmaID: 1
    }
  }

  snimi() {
    this.httpKlijent.post(MojConfig.adresa_servera+"/UposlenikFirme/Snimi",this.uposlenik).subscribe((s:any)=>{
      this.getUposlenici();
    })
  }

  validacija() {
    return this.uposlenik.password==''||
      this.uposlenik.username==''||
      this.uposlenik.ime==''||
      this.uposlenik.prezime==''||
      this.uposlenik.email==''||
      this.uposlenik.pozicija=='';
  }

  editUposlenika(x:any) {
    this.dialogtitle='Edit uposlenika';
    this.uposlenik=x;
  }

  obrisiUposlenika(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+"/UposlenikFirme/Obrisi",x).subscribe((s:any)=>{
      this.getUposlenici();
    })
  }
}
