import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../MojConfig";
import {HttpClient} from "@angular/common/http";
import {MatDialog} from "@angular/material/dialog";

@Component({
  selector: 'app-uposlenici-stipenditora',
  templateUrl: './uposlenici-stipenditora.component.html',
  styleUrls: ['./uposlenici-stipenditora.component.css']
})
export class UposleniciStipenditoraComponent implements OnInit {
  uposleniciPodaci: any;
  uposlenik:any;
  displayedColumns: string[] = ['ime', 'prezime', 'email','stipenditor','akcije'];
  dialogtitle: any;
  filter_imePrezime= '';
  stipenditoriPodaci: any;

  constructor(private httpKlijent:HttpClient,private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getUposlenici();
    this.getStipenditori();
  }

  getUposlenici() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/UposlenikStipenditora/GetAll").subscribe(((x: any) => {
      this.uposleniciPodaci = x;
    }));
  }
  getStipenditori() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/F/GetAll").subscribe(((x: any) => {
      this.stipenditoriPodaci = x;
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
      stipenditorID: 1
    }
  }

  snimi() {
    this.httpKlijent.post(MojConfig.adresa_servera+"/UposlenikStipenditora/Snimi",this.uposlenik).subscribe((s:any)=>{
      this.getUposlenici();
    })
  }

  validacija() {
    return this.uposlenik.password==''||
      this.uposlenik.username==''||
      this.uposlenik.ime==''||
      this.uposlenik.prezime==''||
      this.uposlenik.email=='';
  }

  editUposlenika(x:any) {
    this.dialogtitle='Edit uposlenika';
    this.uposlenik=x;
  }

  obrisiUposlenika(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+"/UposlenikStipenditora/Obrisi",x).subscribe((s:any)=>{
      this.getUposlenici();
    })
  }
}
