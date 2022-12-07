import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../MojConfig";
import {HttpClient} from "@angular/common/http";
import {MatDialog} from '@angular/material/dialog';

@Component({
  selector: 'app-studenti',
  templateUrl: './studenti.component.html',
  styleUrls: ['./studenti.component.css']
})
export class StudentiComponent implements OnInit {
  studentiPodaci: any;
  student:any;
  displayedColumns: string[] = ['brojIndeksa', 'ime', 'prezime', 'fakultet', 'nacinStudiranja','godinaStudija','akcije'];
  dialogtitle: any;
  filter_brojIndeksa= '';
  filter_imePrezime= '';
  fakultetiPodaci: any;

  constructor(private httpKlijent:HttpClient,private dialog: MatDialog) {

  }

  ngOnInit(): void {
    this.getStudenti();
    this.getFakulteti();
  }

  getStudenti() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Student/GetAll").subscribe(((x: any) => {
      this.studentiPodaci = x;
    }));
  }

  getFakulteti() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/F/GetAll").subscribe(((x: any) => {
      this.fakultetiPodaci = x;
    }));
  }
  openDialog(templateRef:any) {
    this.dialog.open(templateRef, {
      width:'40%'
    });
  }

  getpodaci(){
    if(this.studentiPodaci==null)
      return [];
    return this.studentiPodaci.filter((s:any)=>
      ((s.ime+' '+s.prezime).startsWith(this.filter_imePrezime)||
        (s.prezime+' '+s.ime).startsWith(this.filter_imePrezime))
      &&(s.broj_indeksa).startsWith(this.filter_brojIndeksa)
    );
  }

  dodajStudenta() {
    this.dialogtitle='Dodaj studenta';
    this.student={
      id:0,
      password:'',
      username:'',
      ime:'',
      prezime:'',
      broj_indeksa:'',
      godinaStudija: 0,
      nacin_studiranja:'Redovan student',
      fakultetID: 1
    }
  }

  snimi() {
    this.httpKlijent.post(MojConfig.adresa_servera+"/Student/Snimi",this.student).subscribe((s:any)=>{
      this.getStudenti();
    })
  }

  validacija() {
    return this.student.password==''||
      this.student.username==''||
      this.student.ime==''||
      this.student.prezime==''||
      this.student.broj_indeksa==''||
      this.student.godinaStudija==0;
  }

  editStudenta(s:any) {
    this.dialogtitle='Edit studenta';
    this.student=s;
  }

  obrisiStudenta(s:any) {
    this.httpKlijent.post(MojConfig.adresa_servera+"/Student/Obrisi",s).subscribe((s:any)=>{
      this.getStudenti();
    })
  }
}
