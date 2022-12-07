import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../MojConfig";
import {HttpClient} from "@angular/common/http";
import {MatDialog} from '@angular/material/dialog';

@Component({
  selector: 'app-izdavaci-smjestaja',
  templateUrl: './izdavaci-smjestaja.component.html',
  styleUrls: ['./izdavaci-smjestaja.component.css']
})
export class IzdavaciSmjestajaComponent implements OnInit {
  izdavaciPodaci: any;
  filter_imePrezime= '';
  displayedColumns: string[] = ['ime', 'prezime', 'email', 'broj_telefona','akcije'];
  dialogtitle: any;
  izdavac:any;

  constructor(private httpKlijent:HttpClient,private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getIzdavaci();
  }
  getIzdavaci() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/IzdavacSmjestaja/GetAll").subscribe(((x: any) => {
      this.izdavaciPodaci = x;
    }));
  }

  getpodaci(){
    if(this.izdavaciPodaci==null)
      return [];
    return this.izdavaciPodaci.filter((s:any)=>
      (s.ime+' '+s.prezime).startsWith(this.filter_imePrezime)||
        (s.prezime+' '+s.ime).startsWith(this.filter_imePrezime)
    );
  }

  snimi() {
    this.httpKlijent.post(MojConfig.adresa_servera+"/IzdavacSmjestaja/Snimi",this.izdavac).subscribe((x:any)=>{
      this.getIzdavaci();
    })
  }

  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'40%'
    });
  }

  dodaj() {
    this.dialogtitle='Dodaj izdavača';
    this.izdavac={
      id:0,
      password:'',
      username:'',
      ime:'',
      prezime:'',
      email:'',
      broj_telefona:''
    }
  }

  editIzdavac(x:any) {
    this.dialogtitle='Edit izdavača';
    this.izdavac=x;
  }

  validacija() {
    return this.izdavac.password==''||
      this.izdavac.username==''||
      this.izdavac.ime==''||
      this.izdavac.prezime==''||
      this.izdavac.email==''||
      this.izdavac.broj_telefona=='';
  }
  obrisi(x:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+"/IzdavacSmjestaja/Obrisi",x).subscribe((x:any)=>{
      this.getIzdavaci();
    })
  }
}
